// #define VERY_VERBOSE
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;
using System.Diagnostics;
using Il2CppTLD.IntBackedUnit;
using System.Collections;

namespace ExamineActionsAPI
{
    public class ExamineActionsAPI : MelonMod
    {
		public static ExamineActionsAPI Instance { get; private set; }
		internal List<IExamineAction> RegisteredExamineActions = new List<IExamineAction>();
		public const bool DISABLE_CUSTOM_INFO = false; // Custom info can be simply disabled because it's pure UI thing (in case it's broken by updates)
		public static readonly  Color BRIGHT_WHITE = new Color(1f, 1f, 1f, 1f);
		public static readonly  Color SHALLOW_WHITE = new Color(1f, 1f, 1f, 0.6275f);
		public static readonly  Color SECONDARY_COLOR = new Color(0.4314f, 0.5137f, 0.4863f, 1f);
		public static readonly  Color WARNING_COLOR = new Color(0.9044f, 0.6985f, 0.3059f);
		public static readonly  Color DISABLED_COLOR = new Color(0.4632f, 0.1465f, 0.1672f);
        public override void OnInitializeMelon()
		{
			Instance = this;

			const bool DEBUG = false;
			if (DEBUG)
			{
				Register(new DebugAction_Wear());
				Register(new DebugAction_Cancel());
				Register(new DebugAction_Materials());
				Register(new DebugAction_Salt());
				Register(new DebugAction_Salt2());
				Register(new DebugAction_Simple());
				Register(new DebugAction_Tool());
			}

			PowderAndLiquidTypesLocator.PreLoad();
			// uConsole.RegisterCommand("eaapi_tool", new Action(() => {
			// 	uConsole.Log(InterfaceManager.GetPanel<Panel_Inventory_Examine>().GetSelectedTool().name);
			// }));

			uConsole.RegisterCommand("output_action_json", new Action(() => {
				DataDrivenGenericAction.DataDrivenGenericAction.LogJsonTemplate();
			}));
			uConsole.RegisterCommand("output_action_json_minimal", new Action(() => {
				DataDrivenGenericAction.DataDrivenGenericAction.LogMinimalJsonTemplate();
			}));
			// ModComponentCompatibility.PatchModComponent(this);

			State = new ExamineActionState();
		}

        public override void OnLateInitializeMelon()
        {
			MelonCoroutines.Start(LoadZippedJsons());
        }

        IEnumerator LoadZippedJsons ()
		{
			 var task = DataDrivenGenericAction.ZippedActionDefLoader.Run();
			 var wait = new WaitForSeconds(1f);
			 while (!task.IsCompleted)
			 	yield return wait;

			if (task.IsFaulted)
			{
				LoggerInstance.Error("Failed to load examine action definition files: ", task.Exception);
			}
			else if (task.IsCompletedSuccessfully)
			{
				foreach (var a in task.Result)
				    Register(a);
			}
		}

        public static void Register (IExamineAction action)
		{
			// Filter out unsupported/bugged interface here
			bool filtered = false;
			// if (action is IExamineActionRequireLiquid) filtered = true;
			// else if (action is IExamineActionProduceLiquid) filtered = true;

			if (filtered)
			{
				MelonLogger.Warning($"{action.Id} is not registered because it uses some features that is still being fixed or worked on.");
				return;
			}

			if (action is IExamineActionHasDependendency ahd)
			{
				if (ahd.MelonDependency != null)
				foreach (var dep in ahd.MelonDependency)
				{
					Span<char> depSpan = new Span<char>(dep.ToCharArray());
					if (depSpan.Contains('.'))
					{
						var author = depSpan.Slice(0, depSpan.IndexOf('.'));
						var name = depSpan.Slice(depSpan.IndexOf('.') + 1);
						if (MelonMod.FindMelon(name.ToString(), author.ToString())?.Registered != true)
						{
							Instance.LoggerInstance.Error($"Action {action.Id} not registered because its dependency melon {author}.{name} is not loaded.");
							return;
						}
					}
					else
					{
						if (!MelonMod.RegisteredMelons.Any(m => m.Info.Name == dep))
						{
							Instance.LoggerInstance.Error($"Action {action.Id} not registered because its dependency melon {dep} is not loaded.");
							return;
						}
					}
				}

				if (ahd.GearNameDependency != null)
				foreach (var dep in ahd.GearNameDependency)
				{
					if (GearItem.LoadGearItemPrefab(dep) == null)
					{
						Instance.LoggerInstance.Error($"Action {action.Id} not registered because its dependency gear {dep} can not be loaded.");
						return;
					}
				}

				if (ahd.CSharpTypeDependency != null)
				foreach (var dep in ahd.CSharpTypeDependency)
				{
					if (Type.GetType(dep) == null)
					{
						Instance.LoggerInstance.Error($"Action {action.Id} not registered because its dependency CSharp type {dep} can not be loaded.");
						return;
					}
				}
			}

			Instance.RegisteredExamineActions.Add(action);
			Instance.LoggerInstance.Msg($"Action registered: {action.Id}");
		}

		public static void TryRegisterWithJson (params string[] jsons)
		{
			foreach (string json in jsons)
			{
				var action = DataDrivenGenericAction.DataDrivenGenericAction.NewWithJson(json);
				if (action != null)
				{
					Register(action);
				}
			}
		}

		[Conditional("VERY_VERBOSE")]
		internal static void VeryVerboseLog (string msg, params object[] args)
		{
			Instance.LoggerInstance.Msg(msg, args);
		}

		internal ExamineActionState State { get; private set; }
		internal IExamineAction? LastTriedToPerformedCache { get; set; }
		internal int SelectedCustomMenuItemIndex { get; set; } = -1;
		internal List<Panel_Inventory_Examine_MenuItem> OfficialActionMenuItems = new List<Panel_Inventory_Examine_MenuItem>();
		internal List<Panel_Inventory_Examine_MenuItem> CustomActionMenuItems = new List<Panel_Inventory_Examine_MenuItem>();
		internal List<UILabel> CustomActionMenuItemSubLabels = new List<UILabel>();
		internal List<IExamineAction> AvailableCustomActions = new List<IExamineAction>();
		internal IExamineActionPanel DefaultPanel { get; set; }


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
			RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			if (State.Action.CustomPanel != null)
			{
				if (State.Action.CustomPanel.IsExtension) State.PanelExtension = State.Action.CustomPanel;
				else State.Panel = State.Action.CustomPanel;
			}
			else State.Panel = DefaultPanel;
			State.Panel.OnActionSelected(State);
			State.PanelExtension?.OnActionSelected(State);
			State.Panel.Toggle(true);
			State.PanelExtension?.Toggle(true);

			if (pie.m_ReadPanel.active)
				pie.m_ReadPanel.SetActive(false);
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
			State.PanelExtension?.OnActionDeselected(State);
			State.Action = null;
			// VeryVerboseLog($"-DeselectActiveCustomAction");
		}

		public void OnNextSubAction ()
		{
			if (State.Action == null)
				return;
			// VeryVerboseLog($"+OnNextSubAction {State.SubActionId}");
            int max = State.Action.GetSubActionCount(State);
            if (State.SubActionId + 1 < max)
			{
				State.SubActionId += 1;
				State.Recalculate();
				State.Action.OnActionSelected(State);
				State.Panel.OnActionSelected(State);
				State.PanelExtension?.OnActionSelected(State);
				RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			}
			// VeryVerboseLog($"-OnNextSubAction {State.SubActionId}");
		}
		public void OnPreviousSubAction ()
		{
			if (State.Action == null)
				return;
			// VeryVerboseLog($"+OnPreviousSubAction {State.SubActionId}");
            if (State.SubActionId - 1 >= 0)
			{
				State.SubActionId -= 1;
				State.Recalculate();
				State.Action.OnActionSelected(State);
				State.Panel.OnActionSelected(State);
				State.PanelExtension?.OnActionSelected(State);
				RefreshCustomActionMenuItemState(SelectedCustomMenuItemIndex);
			}
			// VeryVerboseLog($"-OnPreviousSubAction {State.SubActionId}");
		}

		public void TryPerformSelectedAction ()
		{
			if (State?.Action != null)
			{
				OnPerformSelectedAction();
			}
		}

		public void OnPerformSelectedAction ()
		{
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			
			PerformingBlockedReased? reason = null;
			if (!State.Action.CanPerform(State))
			{
				reason = PerformingBlockedReased.Action;
			}

            if (State.Action.ShouldConsumeOnSuccess(State))
            {
				if (State.Action.GetConsumingUnits(State) > (State.Subject.m_StackableItem?.m_Units ?? 1)
				 || State.Action.GetConsumingPowderKgs(State) > (State.Subject.m_PowderItem?.m_Weight.ToQuantity(1f) ?? 0f)
				 || State.Action.GetConsumingLiquidLiters(State) > (State.Subject.m_LiquidItem?.m_Liquid.ToQuantity(1f) ?? 0f))
				reason = PerformingBlockedReased.SubjectShortage;
            }

			if (State.Action is IExamineActionHasExternalConstraints constraints)
			{
				var indoorStateConstraints = constraints.GetRequiredIndoorState(State);
				if (indoorStateConstraints != Weather.IndoorState.NotSet && indoorStateConstraints != GameManager.GetWeatherComponent().m_IsIndoors)
					reason = PerformingBlockedReased.IndoorStateConstraint;
				if (!constraints.IsValidTime(State, new (GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes())))
				    reason = PerformingBlockedReased.TimeConstraint;
				if (!constraints.IsValidWeather(State, GameManager.GetWeatherComponent()))
					reason = PerformingBlockedReased.WeatherConstraint;
				if (!constraints.IsPointingToValidObject(State, GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(2)))
					reason = PerformingBlockedReased.PointedObjectConstraint;
			}
	
			if (State.Action is IExamineActionInterruptable interruptable && ExamineActionsAPI.Instance.ShouldInterrupt(interruptable))
			{
				reason = PerformingBlockedReased.Interruption;
			}

			if (!State.ActiveActionMaterialRequirementsMet.Value)
			{
				reason = PerformingBlockedReased.MaterialRequirement;
			}
			if (!State.ActiveActionToolRequirementsMet.Value)
			{
				reason = PerformingBlockedReased.ToolRequirement;
			}
	
			if (State.Action is IExamineActionRequireTool)
			{
				if (State.SelectingTool && State.SelectedTool != null && State.SelectedTool.GetNormalizedCondition() <= 0)
				{
					reason = PerformingBlockedReased.ToolRequirement;
					return;
				}
				else
				{
					if (pie.m_RepairToolsList.m_Tools.Count > 0)
					{
						State.SelectingTool = true;
						State.Panel.OnSelectingTool(State);
						State.PanelExtension?.OnSelectingTool(State);
						pie.SelectWindow(pie.GetActionToolSelect());
					}
					return;
				}
			}
			
			if (reason != null)
			{
				GameAudioManager.PlayGUIError();
				State.Panel.OnPerformingBlocked(State, reason.Value);
				State.PanelExtension?.OnPerformingBlocked(State, reason.Value);
				return;
			}

			LastTriedToPerformedCache = State.Action;
			if (State.Action is IExamineActionRequireTool && State.SelectingTool)
			{
				VeryVerboseLog($"GetSelectedTool {pie.m_RepairToolsList.GetSelectedTool()?.name}");
				State.SelectingTool = false;
				State.Panel.OnToolSelected(State);
				State.PanelExtension?.OnToolSelected(State);
				PerformAction();
				pie.SelectWindow(pie.m_ActionInProgressWindow);
			}
			else
			{
				State.SelectingTool = false;
				PerformAction();
				pie.SelectWindow(pie.m_ActionInProgressWindow);
			}
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
			pie.SetInventoryAlpha(0);
			// pie.m_ProgressBarTimeSeconds = SelectedCustomAction.CalculateProgressSeconds(State);
			// pie.AccelerateTimeOfDay(duration, false);
			State.ActionInProgress = true;

			// if (State.Action is not IExamineActionCancellable)
			// 	InterfaceManager.GetPanel<Panel_HUD>().m_ClickHoldCancelButton.transform.parent.parent.gameObject.SetActive(false); // Force my way to hide that damn button
			State.ActiveResult = ActionResult.Success;
			State.StartedAtRealtime = Time.realtimeSinceStartup;
			State.StartedAtGameTime = new (GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes());
			if (State.Action is IExamineActionFailable)
				State.ActiveResult = (UnityEngine.Random.Range(0f, 100f) <= State.ActiveSuccessChance) ? ActionResult.Success : ActionResult.Failure;
            Panel_GenericProgressBar gpb = pie.m_GenericProgressBar.GetPanel();
			var audio = State.Action.GetAudioName(State);
			if (audio != null)
				gpb.Launch(
					State.Action.ActionButtonLocalizedString.Text(),
					State.Action.CalculateProgressSeconds(State),
					State.ActiveActionDurationMinutes.Value,
					State.ActiveResult.Value == ActionResult.Success? 1 : UnityEngine.Random.Range(0.2f, 0.8f),
					audio,
					null,
					false,
					true,
					new System.Action<bool, bool, float>(ActionCallback)
				);
			else
				gpb.Launch(
					State.Action.ActionButtonLocalizedString.Text(),
					State.Action.CalculateProgressSeconds(State),
					State.ActiveActionDurationMinutes.Value,
					State.ActiveResult.Value == ActionResult.Success? 1 : UnityEngine.Random.Range(0.2f, 0.8f),
					true,
					new System.Action<bool, bool, float>(ActionCallback)
				);

			this.LoggerInstance.Msg($"Performing custom action {State.Action.Id}... ({State.StartedAtGameTime})");
			State.Panel.OnPerformingAction(State);
			State.PanelExtension?.OnPerformingAction(State);
			State.Action.OnPerforming(State);
			VeryVerboseLog($"->>>>>>>>>>>>>>PerformAction ({ GameManager.m_TimeOfDay.GetMinutes() }m");
		}

		internal void ActionCallback (bool success, bool cancel, float progress)
		{
			VeryVerboseLog($"ActionCallback success: {success} / cancel: {cancel} / progress: {progress}");
			State.NormalizedProgress = progress;
			if (success)
				OnActionSucceed();
			else if (cancel && State.InterruptionFlag)
				OnActionInterrupted(State.InterruptionSystemFlag);
			else if (cancel)
				OnActionCancelled();
			else
				OnActionFailed();

			State.InterruptionFlag = false;
		}

		internal void OnActionFinished ()
		{
			VeryVerboseLog($"+>>>>>>>>>>>>>>OnActionFinished ({ GameManager.m_TimeOfDay.GetMinutes() }m");
            TLDDateTimeEAPI now = new(GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes());
            this.LoggerInstance.Msg($"Finished custom action ({now})");
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
				GameObject selectedTool = pie.m_RepairToolsList.GetSelectedTool();
				var degrade = selectedTool?.GetComponent<GearItem>()?.m_DegradeOnUse;
				if (degrade)
				{
					var cache = degrade.m_DegradeHP;
					degrade.m_DegradeHP *= toolUser.GetDegradingScale(State);
					pie.DegradeToolUsedForAction();
					degrade.m_DegradeHP = cache;
				}
			}
			

			pie.m_Slider_ActionProgress.gameObject.SetActive(false);
			// pie.RestoreTimeOfDay();
			State.ActionInProgress = false;
			// VeryVerboseLog($"-OnActionFinished");
		}

		void PostActionFinished ()
		{
			State.StartedAtRealtime = null;
			State.StartedAtGameTime = null;
			State.NormalizedProgress = null;
			State.InterruptionFlag = false;
			State.InterruptionSystemFlag = false;

			// because we are blocking UpdateLabels
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			pie.m_Stack_Label.text = "x" + (State.Subject.m_StackableItem?.m_Units ?? 1);
		}

		internal void OnActionSucceed ()
		{
			// VeryVerboseLog($"+OnActionSucceed");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			OnActionFinished();
			if (State.Action is IExamineActionRequireItems arm) ConsumeItems(arm, ActionResult.Success);
			if (State.Action is IExamineActionRequireLiquid arl) ConsumeLiquid(arl, ActionResult.Success);
			if (State.Action is IExamineActionRequirePowder arp) ConsumePowder(arp, ActionResult.Success);
			if (State.Action is IExamineActionProduceItems ari) YieldProducts(ari);
			if (State.Action is IExamineActionProduceLiquid apl) YieldLiquidProducts(apl);
			if (State.Action is IExamineActionProducePowder arpd) YieldPowderProducts(arpd);

			var consumed = false;
			var destroyed = false;
			if (State.Action.ShouldConsumeOnSuccess(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
			State.Action.OnSuccess(State);
			State.Panel.OnActionSucceed(State);
			State.PanelExtension?.OnActionSucceed(State);
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
			if (State.Action is IExamineActionRequireItems arm) ConsumeItems(arm, ActionResult.Failure);
			if (State.Action is IExamineActionRequireLiquid arl) ConsumeLiquid(arl, ActionResult.Failure);
			if (State.Action is IExamineActionRequirePowder arp) ConsumePowder(arp, ActionResult.Failure);

			HUDMessage.AddMessage(Localization.Get("GAMEPLAY_Failed"), false, false);
			GameAudioManager.PlayGUIError();

            IExamineActionFailable? eaf = (State.Action as IExamineActionFailable);
			var consumed = false;
			var destroyed = false;
			if (eaf.ShouldConsumeOnFailure(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
            eaf.OnActionFailure(State);
			State.Panel.OnActionFailed(State);
			State.PanelExtension?.OnActionFailed(State);
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
		internal void OnActionInterrupted (bool system)
		{
			VeryVerboseLog($"+OnActionInterrupted");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

			OnActionFinished();
			GameAudioManager.PlayGUIError();

			if (system) State.Action.OnActionInterruptedBySystem(State);

			State.ActiveResult = ActionResult.Interruption;
            
			IExamineActionInterruptable? interruptable = State.Action as IExamineActionInterruptable;
			if (interruptable == null) // Not a regular interruption, likely due to emmergencies like wolf attacks
			{
				PostActionFinished();
				return;
			}

			if (State.Action is IExamineActionRequireItems arm) ConsumeItems(arm, ActionResult.Interruption);
			if (State.Action is IExamineActionRequireLiquid arl) ConsumeLiquid(arl, ActionResult.Interruption);
			if (State.Action is IExamineActionRequirePowder arp) ConsumePowder(arp, ActionResult.Interruption);
	
			var consumed = false;
			var destroyed = false;
			if (interruptable.ShouldConsumeOnInterruption(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
			interruptable.OnInterruption(State);
			State.Panel.OnActionInterrupted(State, system);
			State.PanelExtension?.OnActionInterrupted(State, system);
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
			State.ActiveResult = ActionResult.Cancellation;

			GameAudioManager.PlayGUIError();

            IExamineActionCancellable? cancellable = State.Action as IExamineActionCancellable;
			var consumed = false;
			var destroyed = false;
			if (cancellable.ShouldConsumeOnCancellation(State))
			{
				consumed = true;
				destroyed = ConsumeSubject();
			}
            cancellable.OnActionCancellation(State);
			State.Panel.OnActionCancelled(State);
			State.PanelExtension?.OnActionCancelled(State);
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

		internal bool ConsumeSubject ()
		{
			// VeryVerboseLog($"+ConsumeSubject");
			Inventory inv = GameManager.GetInventoryComponent();
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			if (State.Subject.m_StackableItem)
			{
				State.Subject.m_StackableItem.m_Units -= State.Action.GetConsumingUnits(State);
				if (State.Subject.m_StackableItem.m_Units <= 0)
				{
					pie.MaybeReturnAmmoOrFuelToInventory(State.Subject);
					inv.DestroyGear(State.Subject);
					// VeryVerboseLog($"-ConsumeSubject");
					return true;
				}
			}
			else if (State.Subject.m_PowderItem)
			{
				State.Subject.m_PowderItem.RemovePowder(ItemWeight.FromKilograms(State.Action.GetConsumingPowderKgs(State)));
				if (State.Subject?.m_PowderItem != null && State.Subject.m_PowderItem.m_Weight.m_Units <= 0)
				{
					pie.MaybeReturnAmmoOrFuelToInventory(State.Subject);
					inv.DestroyGear(State.Subject);
					// VeryVerboseLog($"-ConsumeSubject");
					return true;
				}
			}
			else if (State.Subject.m_LiquidItem)
			{
				State.Subject.m_LiquidItem.RemoveLiquid(ItemLiquidVolume.FromLiters(State.Action.GetConsumingLiquidLiters(State)), out _);
				if (State.Subject?.m_LiquidItem != null && State.Subject.m_LiquidItem.m_Liquid.m_Units <= 0)
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

		internal void ConsumeItems (IExamineActionRequireItems act, ActionResult result)
		{
			// VeryVerboseLog($"+ConsumeMaterials");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			Inventory inv = GameManager.GetInventoryComponent();
            List<MaterialOrProductItemConf> items = new (1);
			act.GetMaterialItems(State, items);
			if (items == null) return;
            for (int i = 0; i < items.Count; i++)
			{
                byte chance = items[i].Chance;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						SafeRemoveFromInventory(items[i].GearName, items[i].Units);
					}
					else
                        this.LoggerInstance.Msg($"{items[i].GearName} x{items[i].Units} is kept because the {items[i].Chance}% roll didn't pass.");
				}
				else
					SafeRemoveFromInventory(items[i].GearName, items[i].Units);
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
					if (it == State.Subject) continue;
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

		internal void ConsumeLiquid (IExamineActionRequireLiquid act, ActionResult result)
		{
			// VeryVerboseLog($"+ConsumeLiquid");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			Inventory inv = GameManager.GetInventoryComponent();
            List<MaterialOrProductLiquidConf> liquids = new (1);
			act.GetMaterialLiquid(State, liquids);
			if (liquids == null) return;
            for (int i = 0; i < liquids.Count; i++)
			{
                var conf = liquids[i];
                if (conf.Chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= conf.Chance)
					{
						RemoveLiquidFromInventory(conf.Type, conf.Liters);
						VeryVerboseLog($"{conf.Type.name} x{conf.Liters}l is yielded.");
					}
					else
						this.LoggerInstance.Msg($"{conf.Type.name} x{conf.Liters} is kept because the {conf.Chance}% roll didn't pass.");
				}
				else
				{
					RemoveLiquidFromInventory(conf.Type, conf.Liters);
					VeryVerboseLog($"{conf.Type.name} x{conf.Liters}l is yielded.");
				}
			}
			// VeryVerboseLog($"-ConsumePowder");

			void RemoveLiquidFromInventory (LiquidType type, float liters)
			{
				for (int i = 0 ; i < inv.m_Items.Count; i++)
				{
					var gi = inv.m_Items[i]?.m_GearItem;
					var li = gi?.m_LiquidItem;
					if (gi == State.Subject || li == null || li.m_LiquidType != type) continue;

                    float taking = Mathf.Min(liters, li.m_Liquid.ToQuantity(li.LiquidType.Density));
					li.RemoveLiquid(Il2CppTLD.IntBackedUnit.ItemLiquidVolume.FromLiters(taking), out _);
                    liters -= taking;
					if (liters <= 0) break;
				}

				if (liters > 0)
					this.LoggerInstance.Error($"Failed to remove enough liquid from inventory ({type.name}), please report to the author of EAAPI.");
			}
		}

		internal void ConsumePowder (IExamineActionRequirePowder act, ActionResult result)
		{
			// VeryVerboseLog($"+ConsumePowder");
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			Inventory inv = GameManager.GetInventoryComponent();
            List<MaterialOrProductPowderConf> powders = new (1);
			act.GetMaterialPowder(State, powders);
			if (powders == null) return;
            for (int i = 0; i < powders.Count; i++)
			{
                var conf = powders[i];
                if (conf.Chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= conf.Chance)
					{
						RemovePowderFromInventory(conf.Type, conf.Kgs);
						VeryVerboseLog($"{conf.Type.name} x{conf.Kgs}kg is yielded.");
					}
					else
						this.LoggerInstance.Msg($"{conf.Type.name} x{conf.Kgs} is kept because the {conf.Chance}% roll didn't pass.");
				}
				else
				{
					RemovePowderFromInventory(conf.Type, conf.Kgs);
					VeryVerboseLog($"{conf.Type.name} x{conf.Kgs}kg is yielded.");
				}
			}
			// VeryVerboseLog($"-ConsumePowder");

			/// <summary>
			/// Because modder can now define almost anything as materials
			/// we need to make sure ammo or fuel are removed from them
			/// </summary>
			void RemovePowderFromInventory (PowderType type, float units)
			{
				for (int i = 0 ; i < inv.m_Items.Count; i++)
				{
					var gi = inv.m_Items[i]?.m_GearItem;
					var pi = gi?.m_PowderItem;
					if (gi == State.Subject || pi == null || pi.m_Type != type) continue;

					
                    float taking = Mathf.Min(units, pi.m_Weight.m_Units);
					pi.m_Weight = Il2CppTLD.IntBackedUnit.ItemWeight.FromKilograms(pi.m_Weight.m_Units - taking);
                    units -= taking;
					if (units <= 0) break;
				}

				if (units > 0)
					this.LoggerInstance.Error($"Failed to remove enough powder from inventory ({type.name}), please report to the author of EAAPI.");
			}
		}

		internal void YieldProducts (IExamineActionProduceItems act)
		{
			// VeryVerboseLog($"+YieldProducts");
			Inventory inv = GameManager.GetInventoryComponent();
			PlayerManager pm = GameManager.GetPlayerManagerComponent();
            List<MaterialOrProductItemConf> products = new (1);
			act.GetProducts(State, products);
			if (products == null) return;
            for (int pi = 0; pi < products.Count; pi++)
			{
				GearItem prefab = act.OverrideProductPrefab(State, pi);
				if (prefab == null) prefab = GearItem.LoadGearItemPrefab(products[pi].GearName);
                if (products[pi].Chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= (byte)products[pi].Chance)
					{
						var p = pm.InstantiateItemInPlayerInventory(prefab, products[pi].Units, 1, PlayerManager.InventoryInstantiateFlags.EnableNotificationFlag);
						act.PostProcessProduct(State, pi, p);
						VeryVerboseLog($"{products[pi].GearName} x{products[pi].Units} is yielded because the {products[pi].Chance} roll passed.");
					}
				}
				else
				{
					var p = pm.InstantiateItemInPlayerInventory(prefab, products[pi].Units, 1, PlayerManager.InventoryInstantiateFlags.EnableNotificationFlag);
					act.PostProcessProduct(State, pi, p);
					VeryVerboseLog($"{products[pi].GearName} x{products[pi].Units} is yielded.");
				}
			}
			// VeryVerboseLog($"-YieldProducts");
		}
		internal void YieldLiquidProducts (IExamineActionProduceLiquid act)
		{
			// VeryVerboseLog($"+YieldProducts");
			Inventory inv = GameManager.GetInventoryComponent();
			PlayerManager pm = GameManager.GetPlayerManagerComponent();
            List<MaterialOrProductLiquidConf> liquids = new();
			act.GetProductLiquid(State, liquids);
            for (int pi = 0; pi < liquids.Count; pi++)
			{
                MaterialOrProductLiquidConf conf = liquids[pi];
                byte chance = conf.Chance;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						AddLiquidToInventory(conf.Liters, conf.Type);
                        VeryVerboseLog($"{conf.Type.name} {conf.Liters}l is yielded because the {conf.Chance} roll passed.");
					}
				}
				else
				{
					AddLiquidToInventory(conf.Liters, conf.Type);
					VeryVerboseLog($"{conf.Type.name} x{conf.Liters}l is yielded.");
				}
			}
			// VeryVerboseLog($"-YieldProducts");

			void AddLiquidToInventory (float liters, LiquidType type)
			{
				for (int i = 0 ; i < inv.m_Items.Count; i++)
				{
					var gi = inv.m_Items[i]?.m_GearItem;
					var li = gi?.m_LiquidItem;
					if (li == null
					 || li.m_LiquidType != type) continue;


                    float adding = Mathf.Min(liters, (li.GetCapacityLitres() - li.GetVolumeLitres()).ToQuantity(1));
					li.m_Liquid = li.m_Liquid + Il2CppTLD.IntBackedUnit.ItemLiquidVolume.FromLiters(adding);
                    liters -= adding;
					if (liters <= 0) break;
				}

				while (liters > 0)
				{
					var newLiquid = pm.InstantiateItemInPlayerInventory(type.DefaultContainer.m_PrefabReference.GetOrLoadTypedAsset(), 1, 1, PlayerManager.InventoryInstantiateFlags.EnableNotificationFlag);
                    float adding = Mathf.Min(liters, newLiquid.m_LiquidItem.GetCapacityLitres().ToQuantity(1));
					newLiquid.m_LiquidItem.m_Liquid = ItemLiquidVolume.FromLiters(adding);
					liters -= adding;
				}
			}
		}

		internal void YieldPowderProducts (IExamineActionProducePowder act)
		{
			// VeryVerboseLog($"+YieldProducts");
			Inventory inv = GameManager.GetInventoryComponent();
			PlayerManager pm = GameManager.GetPlayerManagerComponent();
            List<MaterialOrProductPowderConf> powders = new();
			act.GetProductPowder(State, powders);
            for (int pi = 0; pi < powders.Count; pi++)
			{
                MaterialOrProductPowderConf conf = powders[pi];
                byte chance = conf.Chance;
                if (chance < 100)
				{
					if (UnityEngine.Random.Range(0, 100) <= chance)
					{
						var p = pm.AddPowderToInventory(ItemWeight.FromKilograms(conf.Kgs), conf.Type);
                        VeryVerboseLog($"{conf.Type.name} {conf.Kgs}kg is yielded because the {conf.Chance} roll passed.");
					}
				}
				else
				{
					var p = pm.AddPowderToInventory(ItemWeight.FromKilograms(conf.Kgs), conf.Type);
					VeryVerboseLog($"{conf.Type.name} x{conf.Kgs}kg is yielded.");
				}
			}
			// VeryVerboseLog($"-YieldProducts");
		}

		internal bool ShouldInterrupt (IExamineActionInterruptable act)
		{
            ActionsToBlock? lightReq = act.GetLightRequirementType(State);
            if (lightReq != null && GameManager.GetWeatherComponent().IsTooDarkForAction(lightReq.Value))
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
			if (GameManager.GetConditionComponent().GetNormalizedCondition() < act.NormalizedConditionInterruptThreshold)
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TooWoundedToRead"), false, false);
				return true;
			}
			if (act.InterruptOnNonRiskAffliction && GameManager.GetConditionComponent().HasNonRiskAffliction())
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CannotReadWithAfflictions"), false, false);
				return true;
			}
			if (act.ShouldInterrupt(State))
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

			int subs = act.GetSubActionCount(ExamineActionsAPI.Instance.State);
            bool canPerform = State.CheckCanPerformSelectedAction(act); // If null, means the state is not yet fully loaded
            it.SetDisabled(!canPerform);
			if (subs <= 1)
			{
				VeryVerboseLog($"+RefreshCustomActionMenuItemState {index} {canPerform} {subs}");
				CustomActionMenuItemSubLabels[index].gameObject.SetActive(false); // Hide subactions indicator 
			}
			else
			{
				VeryVerboseLog($"+RefreshCustomActionMenuItemState {index} {canPerform} {subs}");
				CustomActionMenuItemSubLabels[index].text = $"{ExamineActionsAPI.Instance.State.SubActionId + 1}/{subs}";
				var canPerf = 0;
				var canPerfThis = false;
				var cache = ExamineActionsAPI.Instance.State.SubActionId;
				for (int s = 0; s < subs; s++)
				{
					ExamineActionsAPI.Instance.State.SubActionId = s;
					if (act.CanPerform(Instance.State))
					{
						canPerf++;
						if (cache == s) canPerfThis = true;
					}
				}
				ExamineActionsAPI.Instance.State.SubActionId = cache;
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
