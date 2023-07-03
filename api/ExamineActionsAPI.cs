// #define VERY_VERBOSE
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;
using UnityEngine.Playables;
using UnityEngine.AddressableAssets;
using Il2CppAK.Wwise;
using System.Diagnostics;

namespace ExamineActionsAPI
{
    public class ExamineActionsAPI : MelonMod
    {
		public static ExamineActionsAPI Instance { get; private set; }
		internal List<IExamineAction> RegisteredActions = new List<IExamineAction>();
		public const bool DISABLE_CUSTOM_INFO = false; // Custom info can be simply disabled because it's pure UI thing (in case it's broken by updates)
        public override void OnInitializeMelon()
		{
			Instance = this;

			const bool DEBUG = false;
			if (DEBUG)
			{
				Register(new DebugAction_Cancel());
				Register(new DebugAction_Materials());
				Register(new DebugAction_Salt());
				Register(new DebugAction_Tool());
				Register(new DebugAction_Simple());
			}

			// uConsole.RegisterCommand("eaapi_tool", new Action(() => {
			// 	uConsole.Log(InterfaceManager.GetPanel<Panel_Inventory_Examine>().GetSelectedTool().name);
			// }));

			// uConsole.RegisterCommand("eaapi_btn_idx", new Action(() => {
			// 	uConsole.Log(InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_SelectedButtonIndex.ToString());
			// }));
			// uConsole.RegisterCommand("eaapi_act_chance", new Action(() => {
			// 	uConsole.Log(InterfaceManager.GetPanel<Panel_Inventory_Examine>().GetChanceActionSuccess(0).ToString());
			// }));

			State = new ExamineActionState();
		}

		public static void Register (IExamineAction action)
		{
			// Filter out unsupported/bugged interface here
			bool filtered = false;
			if (action is IExamineActionProduceLiquid) filtered = true;
			else if (action is IExamineActionProduceLiquid) filtered = true;

			if (filtered)
			{
				MelonLogger.Warning($"{action.Id} is not registered because it uses some functionalities that is still being fixed or worked on.");
				return;
			}

			Instance.RegisteredActions.Add(action);
		}

		[Conditional("VERY_VERBOSE")]
		internal static void VeryVerboseLog (string msg)
		{
			Instance.LoggerInstance.Msg(msg);
		}

		internal ExamineActionState State { get; private set; }
		internal IExamineAction? LastTriedToPerformedCache { get; set; }
		internal int SelectedCustomMenuItemIndex { get; set; } = -1;
		internal List<Panel_Inventory_Examine_MenuItem> OfficialActionMenuItems = new List<Panel_Inventory_Examine_MenuItem>();
		internal List<Panel_Inventory_Examine_MenuItem> CustomActionMenuItems = new List<Panel_Inventory_Examine_MenuItem>();
		internal List<UILabel> CustomActionMenuItemSubLabels = new List<UILabel>();
		internal List<IExamineAction> AvailableCustomActions = new List<IExamineAction>();
		internal IExamineActionPanel DefaultPanel;

		public void OnCustomActionSelected (int index)
		{
			VeryVerboseLog($"+OnCustomActionSelected {index}");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			var act = AvailableCustomActions[index];
			SelectedCustomMenuItemIndex = index;
			State.SelectingTool = false;
			State.Action = act;
			State.Action.OnActionSelected(State);

			State.Recalculate();
			State.Panel = State.Action.CustomPanel?? DefaultPanel;
			State.Panel.OnActionSelected(State);
			State.Panel.Toggle(true);

			if (pie.m_ReadPanel.active) pie.m_ReadPanel.SetActive(false);
			// VeryVerboseLog($"-OnCustomActionSelected {index}");
		}
		internal void DeselectActiveCustomAction ()
		{
			VeryVerboseLog($"+DeselectActiveCustomAction {SelectedCustomMenuItemIndex}");
			State.SubActionId = 0;
			RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			SelectedCustomMenuItemIndex = -1;
			State.Action?.OnActionDeselected(State);
			State.Panel?.OnActionDeselected(State);
			State.Action = null;
			// VeryVerboseLog($"-DeselectActiveCustomAction");
		}

		public void OnNextSubAction ()
		{
			// VeryVerboseLog($"+OnNextSubAction {State.SubActionId}");
            int max = State.Action.GetSubActionCounts(State);
            if (State.SubActionId + 1 < max)
			{
				State.SubActionId += 1;
				State.Recalculate();
				State.Action.OnActionSelected(State);
				State.Panel.OnActionSelected(State);
				RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			}
			// VeryVerboseLog($"-OnNextSubAction {State.SubActionId}");
		}
		public void OnPreviousSubAction ()
		{
			// VeryVerboseLog($"+OnPreviousSubAction {State.SubActionId}");
            if (State.SubActionId - 1 >= 0)
			{
				State.SubActionId -= 1;
				State.Recalculate();
				State.Action.OnActionSelected(State);
				State.Panel.OnActionSelected(State);
				RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			}
			// VeryVerboseLog($"-OnPreviousSubAction {State.SubActionId}");
		}

		public void OnPerformSelectedAction ()
		{
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			VeryVerboseLog($"+OnPerformSelectedAction");
			if (!State.Action.CanPerform(State) || !State.ActiveActionRequirementsMet.Value)
			{
				GameAudioManager.PlayGUIError();
				return;
			}
			LastTriedToPerformedCache = State.Action;
			if (State.Action is IExamineActionRequireTool)
			{
				if (State.SelectingTool)
				{
					VeryVerboseLog($"GetSelectedTool {pie.GetSelectedTool()?.name}");
					State.SelectingTool = false;
					State.Panel.OnToolSelected(State);
					PerformAction();
					pie.SelectWindow(pie.m_ActionInProgressWindow);
				}
				else
				{
					State.SelectingTool = true;
					State.Panel.OnSelectingTool(State);
					pie.SelectWindow(pie.m_ActionToolSelect);
				}
			}
			else
			{
				State.SelectingTool = false;
				PerformAction();
				pie.SelectWindow(pie.m_ActionInProgressWindow);
			}
			VeryVerboseLog($"-OnPerformSelectedAction");
		}

		internal void PerformAction ()
		{
			// VeryVerboseLog($"+PerformAction");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			if (GameManager.GetPlayerManagerComponent().m_ItemInHands == State.Subject)
			{
				GameManager.GetPlayerManagerComponent().UnequipItemInHands();
			}
			GameAudioManager.PlayGuiConfirm();

			pie.SelectWindow(pie.m_ActionInProgressWindow);
			pie.m_Slider_ActionProgress.gameObject.SetActive(true);
			pie.m_Slider_ActionProgress.value = 0;
			pie.m_ElapsedProgressBarSeconds = 0;
			pie.m_GearItem.SetHaltDecay(true);
			InterfaceManager.GetPanel<Panel_Inventory>().GetComponent<UIPanel>().alpha = 0f;
			// pie.m_ProgressBarTimeSeconds = SelectedCustomAction.CalculateProgressSeconds(State);
			// pie.AccelerateTimeOfDay(duration, false);
			State.ActionInProgress = true;

			// if (State.Action is not IExamineActionCancellable)
			// 	InterfaceManager.GetPanel<Panel_HUD>().m_ClickHoldCancelButton.transform.parent.parent.gameObject.SetActive(false); // Force my way to hide that damn button
			State.ActiveResult = true;
			State.StartedRealtime = Time.realtimeSinceStartup;
			State.StartedGameTime = new (GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes());
			if (State.Action is IExamineActionFailable)
				State.ActiveResult = UnityEngine.Random.Range(0f, 100f) <= State.ActiveSuccessChance;
            Panel_GenericProgressBar gpb = pie.m_GenericProgressBar.GetPanel();
            gpb.Launch(
                State.Action.ActionButtonLocalizedString.Text(),
                State.Action.CalculateProgressSeconds(State),
                State.ActiveActionDurationMinutes.Value,
                State.ActiveResult.Value? 1 : UnityEngine.Random.Range(0.2f, 0.8f),
				true,
				new System.Action<bool, bool, float>(ActionCallback)
			);

			State.Panel.OnPerformingAction(State);
			State.Action.OnPerform(State);
			VeryVerboseLog($"->>>>>>>>>>>>>>PerformAction ({ GameManager.m_TimeOfDay.GetMinutes() }m");
		}

		internal void ActionCallback (bool success, bool playerCancel, float progress)
		{
			VeryVerboseLog($"ActionCallback success: {success} / cancel: {playerCancel} / progress: {progress}");
			if (success)
				OnActionSucceed();
			else if (playerCancel)
				OnActionCancelled();
			else if (State.InterruptionFlag)
				OnActionInterrupted();
			else
				OnActionFailed();

			State.InterruptionFlag = false;
		}

		internal void OnActionFinished ()
		{
			VeryVerboseLog($"+>>>>>>>>>>>>>>OnActionFinished ({ GameManager.m_TimeOfDay.GetMinutes() }m");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			
			// InterfaceManager.GetPanel<Panel_HUD>().m_ClickHoldCancelButton.transform.parent.parent.gameObject.SetActive(true); // Force my way to hide that damn button
			if (!pie.m_GearItem || !pie.m_GearItem.gameObject)
			{
				MelonLogger.Error($"Action could not complete. Object was destroyed during action.");
				pie.OnBack();
				return;
			}
			pie.m_GearItem.SetHaltDecay(false);

			if (State.Action is IExamineActionRequireTool toolUser)
			{
				GameObject selectedTool = pie.GetSelectedTool();
				var degrade = selectedTool?.GetComponent<GearItem>()?.m_DegradeOnUse;
				if (degrade)
				{
					var cache = degrade.m_DegradeHP;
					degrade.m_DegradeHP *= toolUser.CalculateDegradingScale(State);
					pie.DegradeToolUsedForAction();
					degrade.m_DegradeHP = cache;
				}
				else pie.DegradeToolUsedForAction();
			}
			

			pie.m_Slider_ActionProgress.gameObject.SetActive(false);
			pie.RestoreTimeOfDay();
			State.ActionInProgress = false;
			// VeryVerboseLog($"-OnActionFinished");
		}

		void PostActionFinished ()
		{
			State.StartedRealtime = null;
			State.StartedGameTime = null;
		}

		internal void OnActionSucceed ()
		{
			// VeryVerboseLog($"+OnActionSucceed");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			OnActionFinished();
			if (State.Action is IExamineActionRequireMaterials arm) ConsumeMaterials(arm, ActionResult.Success);
			if (State.Action is IExamineActionProduceItems ari) YieldProducts(ari);
			if (State.Action is IExamineActionProduceLiquid arl) YieldLiquidProducts(arl);

			var consumed = false;
			var destroyed = false;
			if (State.Action.ConsumeOnSuccess(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
			State.Action.OnSuccess(State);
			State.Panel.OnActionSucceed(State);
			PostActionFinished();
			if (destroyed)
			{
				State.Subject = null;
				pie.OnBack();
			}
			else pie.SelectWindow(pie.m_MainWindow);
			// VeryVerboseLog($"-OnActionSucceed");
		}

		internal void OnActionFailed ()
		{
			// VeryVerboseLog($"+OnActionFailed");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			OnActionFinished();
			if (State.Action is IExamineActionRequireMaterials arm) ConsumeMaterials(arm, ActionResult.Failure);

			HUDMessage.AddMessage(Localization.Get("GAMEPLAY_Failed"), false, false);
			GameAudioManager.PlayGUIError();

            IExamineActionFailable? eaf = (State.Action as IExamineActionFailable);
			var consumed = false;
			var destroyed = false;
			if (eaf.ConsumeOnFailure(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
            eaf.OnActionFailed(State);
			State.Panel.OnActionFailed(State);
			PostActionFinished();
			if (consumed)
				VeryVerboseLog("The gear is consumed because the action consumes on failure too.");
			if (destroyed)
			{
				State.Subject = null;
				pie.OnBack();
			}
			else pie.SelectWindow(pie.m_MainWindow);
			VeryVerboseLog($"-OnActionFailed");

		}
		internal void OnActionInterrupted ()
		{
			VeryVerboseLog($"+OnActionInterrupted");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			OnActionFinished();
			if (State.Action is IExamineActionRequireMaterials arm) ConsumeMaterials(arm, ActionResult.Interruption);
	
			GameAudioManager.PlayGUIError();

            IExamineActionInterruptable? interruptable = State.Action as IExamineActionInterruptable;
			var consumed = false;
			var destroyed = false;
			if (interruptable.ConsumeOnInterruption(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
			interruptable.OnInterrupted(State);
			State.Panel.OnActionInterrupted(State);
			PostActionFinished();
			if (consumed)
				VeryVerboseLog("The gear is consumed because the action consumes on interruption too.");
			if (destroyed)
			{
				State.Subject = null;
				pie.OnBack();
			}
			else pie.SelectWindow(pie.m_MainWindow);
			// AkSoundEngine.StopPlayingID(this.m_ProgressBarAudio, GameManager.GetGameAudioManagerComponent().m_StopAudioFadeOutMicroseconds);
			VeryVerboseLog($"-OnActionInterrupted");
		}

		internal void OnActionCancelled ()
		{
			// VeryVerboseLog($"+OnActionCancelled");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			OnActionFinished();

			GameAudioManager.PlayGUIError();

            IExamineActionCancellable? cancellable = State.Action as IExamineActionCancellable;
			var consumed = false;
			var destroyed = false;
			if (cancellable.ConsumeOnCancel(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
            cancellable.OnActionCanceled(State);
			State.Panel.OnActionCancelled(State);
			PostActionFinished();
			if (consumed)
				VeryVerboseLog("The gear is consumed because the action consumes on cancellation too.");
			if (destroyed)
			{
				State.Subject = null;
				pie.OnBack();
			}
			else pie.SelectWindow(pie.m_MainWindow);
			// VeryVerboseLog($"-OnActionCancelled");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>destroyed</returns>
		internal bool ConsumeSubject ()
		{
			// VeryVerboseLog($"+ConsumeSubject");
			Inventory inv = GameManager.GetInventoryComponent();
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			if (State.Subject.m_StackableItem)
			{
				State.Subject.m_StackableItem.m_Units--;
				if (State.Subject.m_StackableItem.m_Units == 0)
				{
					pie.MaybeReturnAmmoOrFuelToInventory(State.Subject);
					inv.DestroyGear(State.Subject);
					// VeryVerboseLog($"-ConsumeSubject");
					return true;
				}
			}
			else
			{
				pie.MaybeReturnAmmoOrFuelToInventory(State.Subject);
				inv.DestroyGear(State.Subject);
				// VeryVerboseLog($"-ConsumeSubject");
				return true;
			}

			// VeryVerboseLog($"-ConsumeSubject");
			return false;
		}

		internal void ConsumeMaterials (IExamineActionRequireMaterials act, ActionResult result)
		{
			// VeryVerboseLog($"+ConsumeMaterials");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			Inventory inv = GameManager.GetInventoryComponent();
            (string, int, byte)[] mats = act.GetMaterials(State);
            for (int i = 0; i < mats.Length; i++)
			{
                (string, int, byte) mat = mats[i];
                byte chance = mat.Item3;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						SafeRemoveFromInventory(mat.Item1, mat.Item2);
					}
					else
                        VeryVerboseLog($"{mat.Item1} x{mat.Item2} is kept because the {mat.Item3}% roll didn't pass.");
				}
				else
					SafeRemoveFromInventory(mat.Item1, mat.Item2);
			}
			// VeryVerboseLog($"-ConsumeMaterials");

			/// <summary>
			/// Because modder can now define almost anything as materials
			/// we need to make sure ammo or fuel are removed from them
			/// </summary>
			void SafeRemoveFromInventory (string name, int units)
			{
				while (units > 0)
				{
					var it = inv.GearInInventory(name, units);
					if (it == null)
					{
						MelonLogger.Error($"Failed to consume all materilas, remaining: {units}...");
						break;
					}
					var stackable = it.m_StackableItem;
					if (stackable)
					{
						var toRemove = units > stackable.m_Units? stackable.m_Units : units;
						stackable.m_Units -= toRemove;
						units -= toRemove;
						if (stackable.m_Units == 0)
						{
							pie.MaybeReturnAmmoOrFuelToInventory(it);
							inv.DestroyGear(it);
						}
					}
					else
					{
						pie.MaybeReturnAmmoOrFuelToInventory(it);
						inv.DestroyGear(it);
						units -= 1;
					}
				}
			}
		}
		internal void YieldProducts (IExamineActionProduceItems act)
		{
			// VeryVerboseLog($"+YieldProducts");
			Inventory inv = GameManager.GetInventoryComponent();
			PlayerManager pm = GameManager.GetPlayerManagerComponent();
            (string, int, byte)[] products = act.GetProducts(State);
            for (int pi = 0; pi < products.Length; pi++)
			{
				GearItem prefab = act.OverrideProductPrefabs(State, pi);
				if (prefab == null) prefab = GearItem.LoadGearItemPrefab(products[pi].Item1);
                byte chance = products[pi].Item3;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						var p = pm.InstantiateItemInPlayerInventory(prefab, products[pi].Item2, 1, PlayerManager.InventoryInstantiateFlags.EnableNotificationFlag);
						act.PostProcessProduct(State, pi, p);
						VeryVerboseLog($"{products[pi].Item1} x{products[pi].Item2} is yielded because the {products[pi].Item3} roll passed.");
					}
				}
				else
				{
					var p = pm.InstantiateItemInPlayerInventory(prefab, products[pi].Item2, 1, PlayerManager.InventoryInstantiateFlags.EnableNotificationFlag);
					act.PostProcessProduct(State, pi, p);
				}
			}
			// VeryVerboseLog($"-YieldProducts");
		}
		internal void YieldLiquidProducts (IExamineActionProduceLiquid act)
		{
			// VeryVerboseLog($"+YieldProducts");
			Inventory inv = GameManager.GetInventoryComponent();
			PlayerManager pm = GameManager.GetPlayerManagerComponent();
            List<(GearLiquidTypeEnum type, float units, byte chance)> liquids = new();
			act.GetProductLiquid(State, liquids);
            for (int pi = 0; pi < liquids.Count; pi++)
			{
                (GearLiquidTypeEnum type, float units, byte chance) conf = liquids[pi];
                byte chance = conf.Item3;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						var p = pm.AddLiquidToInventory(conf.units, conf.type);
                        VeryVerboseLog($"{conf.Item1} {conf.Item2}l is yielded because the {conf.Item3} roll passed.");
					}
				}
				else
				{
					var p = pm.AddLiquidToInventory(conf.units, conf.type);
				}
			}
			// VeryVerboseLog($"-YieldProducts");
		}

		internal bool ShouldInterrupt (IExamineActionInterruptable act)
		{
			if (act.LightRequirementType != null && GameManager.GetWeatherComponent().IsTooDarkForAction(act.LightRequirementType.Value))
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooDarkToRead"), false, false);
				return true;
			}
			if (act.InterruptOnExhausted && GameManager.GetFatigueComponent().IsExhausted())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooTiredToRead"), false, false);
				return true;
			}
			if (act.InterruptOnFreezing && GameManager.GetFreezingComponent().IsFreezing())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooColdToRead"), false, false);
				return true;
			}
			if (act.InterruptOnStarving && GameManager.GetHungerComponent().IsStarving())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooHungryToRead"), false, false);
				return true;
			}
			if (act.InterruptOnDehydrated && GameManager.GetThirstComponent().IsDehydrated())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooThirstyToRead"), false, false);
				return true;
			}
			if (GameManager.GetConditionComponent().GetNormalizedCondition() < act.MinimumCondition)
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooWoundedToRead"), false, false);
				return true;
			}
			if (act.InterruptOnNonRiskAffliction && GameManager.GetConditionComponent().HasNonRiskAffliction())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CannotReadWithAfflictions"), false, false);
				return true;
			}
			if (act.ShouldInterruptCustomConditions(State))
			{
				return true;
			}

			return false;
		}

		public void RefreshCustomActionMenuItemState (int index)
		{
			if (index < 0 || index >= ExamineActionsAPI.Instance.AvailableCustomActions.Count) return;
			
			var act = ExamineActionsAPI.Instance.AvailableCustomActions[index];
			var it = ExamineActionsAPI.Instance.CustomActionMenuItems[index];

			int subs = act.GetSubActionCounts(ExamineActionsAPI.Instance.State);
			VeryVerboseLog($"+RefreshCustomActionMenuItemState {index}");
            bool activeActionRequirementsMet = State.ActiveActionRequirementsMet ?? true; // If null, means the state is not yet fully loaded
            bool canPerform = activeActionRequirementsMet && act.CanPerform(Instance.State);
            it.SetDisabled(!canPerform);
			if (subs == 1)
			{
				VeryVerboseLog($"+RefreshCustomActionMenuItemState {index}");
				CustomActionMenuItemSubLabels[index].gameObject.SetActive(false);
			}
			else
			{
				VeryVerboseLog($"+RefreshCustomActionMenuItemState {index}");
				CustomActionMenuItemSubLabels[index].text = $"{ExamineActionsAPI.Instance.State.SubActionId + 1}/{subs}";
				var canPerf = 0;
				var canPerfThis = false;
				var cache = ExamineActionsAPI.Instance.State.SubActionId;
				for (int s = 0; s < subs; s++)
				{
					ExamineActionsAPI.Instance.State.SubActionId = s;
					if (canPerform)
					{
						canPerf++;
						if (cache == s) canPerfThis = true;
					}
				}
				ExamineActionsAPI.Instance.State.SubActionId = cache;
				it.SetDisabled(!canPerfThis);
				if (canPerf == 0)
				{
					CustomActionMenuItemSubLabels[index].color = it.m_TextColor_Disabled;
				}
				else if (canPerf == subs)
				{
					CustomActionMenuItemSubLabels[index].color = it.m_TextColor_Normal;
				}
				else
				{
					CustomActionMenuItemSubLabels[index].color = new Color(0.6397f, 0.5023f, 0.1309f);
				}
				CustomActionMenuItemSubLabels[index].gameObject.SetActive(true);
			}
		}
    }
}
