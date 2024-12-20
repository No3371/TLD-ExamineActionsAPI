// #define VERY_VERBOSE
using Il2Cpp;
using Il2CppTLD.Gear;
using Il2CppTLD.Interactions;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    public class ExamineActionState
	{
        private GearItem? subject;
        private BaseInteraction? subjectInteraction;
        private IExamineAction? action;
        private GearItem? selectedTool;
        private bool actionInProgress;

        public GearItem? Subject
        {
            get => subject;
			internal set
            {
				var cache = subject;
                subject = value;
                subjectInteraction = null;
				ResetActionMeta();
				if (cache != value) ExamineActionsAPI.VeryVerboseLog($"Subject changed: {value?.name}");
            }
        }
        public BaseInteraction? SubjectInteraction
        {
            get => subjectInteraction;
			internal set
            {
				var cache = subjectInteraction;
                subjectInteraction = value;
                subject = null;
				ResetActionMeta();
				if (cache != value) ExamineActionsAPI.VeryVerboseLog($"SubjectInteraction changed: {value?.name}");
            }
        }
        public IExamineAction? Action
        {
            get => action;
			set
            {
				var cache = action;
                action = value;
				ResetActionMeta();
				if (cache != value) ExamineActionsAPI.VeryVerboseLog($"Action changed: {value?.Id}");
            }
        }
        public GearItem? SelectedTool
        {
            get => selectedTool;
			internal set
            {
				var cache = selectedTool;
                selectedTool = value;
				if (cache != value)
				{
					ExamineActionsAPI.VeryVerboseLog($"Selected tool changed: {value?.name} ({value?.GetInstanceID()})");
					if (Action != null) OnToolChanged();
				}
            }
        }
        internal IExamineActionPanel Panel { get; set; }
        internal IExamineActionPanel PanelExtension { get; set; }
        internal bool ActionInProgress
        {
            get => actionInProgress;
			set
            {
				var cache = actionInProgress;
                actionInProgress = value;
				if (cache != value)
					ExamineActionsAPI.VeryVerboseLog($"ActionInProgress changed: {value}");
            }
        }
        internal int? ActiveActionDurationMinutes { get; set; }
		internal float? ActiveSuccessChance { get; set; }
		internal ActionResult? ActiveResult { get; set; }
		internal bool? ActiveActionMaterialRequirementsMet { get; set; }
		internal bool? ActiveActionToolRequirementsMet { get; set; }
		internal bool? AllMaterialsReady { get; set; }
		internal bool InterruptionFlag { get; set; }
		internal bool InterruptionSystemFlag { get; set; }
		internal bool SelectingTool { get; set; }
		/// <summary>
		/// The sub action picked by the player
		/// </summary>
		/// <value></value>
		public int SubActionId { get; internal set; }
		/// <summary>
		/// At what Time.realtimeSinceStartup the action is started
		/// </summary>
		public float? StartedAtRealtime { get; internal set; }
		/// <summary>
		/// At what ingame time the action is started
		/// </summary>
		/// <value></value>
		public TLDDateTimeEAPI? StartedAtGameTime { get; internal set; }
        public int? ElapsedIngameMinutesPerforming
        {
            get
            {
                var now = new TLDDateTimeEAPI(GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes());
                return (now - StartedAtGameTime)?.TotalMinutes;
            }
        }
        public float? ElapsedRealtimeSecondsPerforming
        {
            get
            {
                return Time.realtimeSinceStartup - StartedAtRealtime;
            }
        }
		/// <summary>
		/// The 0-1 percentage of the elapsed execution time
		/// Main purpose is for actions to access how much has been performed on non-success result
		/// </summary>
		/// <value></value>
		public float? NormalizedProgress { get; internal set; }
		public void ResetActionMeta ()
		{
			ExamineActionsAPI.VeryVerboseLog($"ResetActionMeta");
			ActionInProgress = false;
			ActiveActionDurationMinutes = null;
			ActiveSuccessChance = null;
			ActiveResult = null;
			SelectedTool = null;
			InterruptionFlag = false;
			InterruptionSystemFlag = false;
			ActiveActionMaterialRequirementsMet = null;
			SelectingTool = false;
			Panel?.Toggle(false);
			Panel = null;
            PanelExtension = null;
			Temp.Clear();
			SubActionId = 0;
			StartedAtRealtime = null;
			StartedAtGameTime = null;
			NormalizedProgress = null;
		}

		public void OnToolChanged ()
		{
			ActiveActionDurationMinutes = Action.CalculateDurationMinutes(this);
            MaybeUpdateSuccessChance();
			if (SelectingTool) Panel.OnSelectingToolChanged(this);
		}

		public void Recalculate ()
        {
            MaybeUpdateSuccessChance();
            ActiveActionMaterialRequirementsMet = ActiveActionToolRequirementsMet = true;

            AllMaterialsReady = CheckMaterials(this, Action);
			if (!AllMaterialsReady.Value)
                ActiveActionMaterialRequirementsMet = false;

            if (Action is IExamineActionRequireTool eat)
            {
                var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
                pie.m_RepairToolsList.m_Tools.Clear();
                eat.GetToolOptions(this, pie.m_RepairToolsList.m_Tools);
                // for (int i = 0; i < pie.m_RepairToolsList.m_Tools.Count; i++)
                // 	ExamineActionsAPI.VeryVerboseLog($"{pie.m_RepairToolsList.m_Tools[i]?.name} ({pie.m_RepairToolsList.m_Tools[i].GetInstanceID()})");
                if (pie.m_RepairToolsList.m_Tools.Count == 0) // No tool
                {
                    ActiveActionToolRequirementsMet = false;
                }
            }

            ActiveActionDurationMinutes = this.Action.CalculateDurationMinutes(this);
        }
        static Il2CppSystem.Collections.Generic.List<GameObject> temp = new ();
        public bool CheckCanPerformSelectedAction (IExamineAction act)
        {
            var mats = CheckMaterials(this, act);
			if (!mats)
                return false;
            
            if (!act.CanPerform(this))
                return false;

            if (act.ConsumeOnSuccess(this) && act.GetConsumingUnits(this) > (Subject.m_StackableItem?.m_Units ?? 1))
            {
                return false;
            }

            if (act is IExamineActionRequireTool eat)
            {
                temp.Clear();
                eat.GetToolOptions(this, temp);
                if (temp.Count == 0) // No tool
                    return false;
            }

            if (act is IExamineActionHasExternalConstraints constraints)
            {
                var indoorStateConstraints = constraints.GetRequiredIndoorState(this);
                var constraintsSatisfied =
                    (indoorStateConstraints == Weather.IndoorState.NotSet || indoorStateConstraints == GameManager.GetWeatherComponent().m_IsIndoors)
                    && constraints.IsValidTime(this, new (GameManager.m_TimeOfDay.GetDayNumber(), GameManager.m_TimeOfDay.GetHour(), GameManager.m_TimeOfDay.GetMinutes()))
                    && constraints.IsValidWeather(this, GameManager.GetWeatherComponent())
                    && constraints.IsPointingToValidObject(this, GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(2.5f));

                if (!constraintsSatisfied)
                    return false;
            }

            return true;
        }

        private void MaybeUpdateSuccessChance()
        {
            if (Action is IExamineActionFailable failable)
            {
                var successChance = (100f - failable.CalculateFailureChance(this));
                successChance = Mathf.Clamp(successChance, 0f, 100f);
                ActiveSuccessChance = successChance;
            }
        }

        internal static bool CheckMaterials (ExamineActionState state, IExamineAction act)
		{
            List<MaterialOrProductItemConf> materials = null;
            if (act is IExamineActionRequireItems erm)
            {
                materials = new (1);
                erm.GetRequiredItems(state, materials);
				for (int i = 0; i < materials.Count; i++)
				{
					int totalOfTheGearTypeToCheck = materials[i].Units;
					for (int j = 0; j < materials.Count; j++)
						if (i != j && materials[i].GearName == materials[j].GearName) totalOfTheGearTypeToCheck += materials[j].Units;
					
					ExamineActionsAPI.VeryVerboseLog($"Checking for mat: {materials[i].GearName} x{totalOfTheGearTypeToCheck} (x{materials[i].Units})");
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					GameManager.GetInventoryComponent().GearInInventory(materials[i].GearName, gears);
					for (int g = 0; g < gears.Count; g++)
					{
						GearItem gearItem = gears[g];
						if (gearItem == state.Subject) continue; // Exclude the subject

                        int units = gearItem.m_StackableItem?.m_Units ?? 1;
						totalOfTheGearTypeToCheck -= units;
                        ExamineActionsAPI.VeryVerboseLog($"  Found in inv: {gearItem.name} x{units} (...{totalOfTheGearTypeToCheck})");
						if (totalOfTheGearTypeToCheck <= 0) break;
					}
					if (totalOfTheGearTypeToCheck > 0) return false;
				}
            }
            List<MaterialOrProductLiquidConf> liquids = null;
            if (act is IExamineActionRequireLiquid erl)
            {
                liquids = new(1);
                erl.GetRequiredLiquid(state, liquids);
				for (int i = 0; i < liquids.Count; i++)
				{
                    if (liquids[i].Type == null)
                        MelonLogger.Error($"Liquid type provided by action {act.Id} is null, this is a severe error and you should avoid this action for now. Contact the mod author providing the action.");
					float totalLiters = liquids[i].Liters;
					for (int j = 0; j < liquids.Count; j++)
						if (i != j && liquids[i].Type == liquids[j].Type) totalLiters += liquids[j].Liters;
					
					ExamineActionsAPI.VeryVerboseLog($"Checking for liquid: {liquids[i].Type} x{totalLiters} (x{liquids[i].Liters})");
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					var totalInInv = GameManager.GetInventoryComponent().GetTotalLiquidVolume(liquids[i].Type);
                    
					if (state.Subject.m_LiquidItem != null
                    && state.Subject.m_LiquidItem.m_LiquidType == liquids[i].Type)
                        totalInInv -= state.Subject.m_LiquidItem.m_Liquid;
					if (totalLiters > totalInInv.ToQuantity(1)) return false;
					break;
				}
            }
            List<MaterialOrProductPowderConf> powders = null;
            if (act is IExamineActionRequirePowder erp)
            {
                powders = new(1);
                erp.GetRequiredPowder(state, powders);
				for (int i = 0; i < powders.Count; i++)
				{
                    if (powders[i].Type == null)
                        MelonLogger.Error($"Powder type provided by action {act.Id} is null, this is a severe error and you should avoid this action for now. Contact the mod author providing the action.");
					float totalToCheck = powders[i].Kgs;
					for (int j = 0; j < i; j++)
						if (powders[i].Type == powders[j].Type) totalToCheck += powders[j].Kgs;
					
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					var totalInInv = GameManager.GetInventoryComponent().GetTotalPowderWeight(powders[i].Type);
					ExamineActionsAPI.VeryVerboseLog($"Checking for powder: {powders[i].Type} x{totalToCheck} (x{powders[i].Kgs}) (has: {totalInInv})");
					if (state.Subject.m_PowderItem?.m_Type == powders[i].Type) totalInInv -= state.Subject.m_PowderItem.m_Weight;
					if (totalToCheck > totalInInv.ToQuantity(1f)) return false;
					break;
				}
            }
            ExamineActionsAPI.VeryVerboseLog($"  Materials Ready!");
			return true;
		}


        internal void GetAllMaterials(out List<MaterialOrProductItemConf>? items, out List<MaterialOrProductLiquidConf>? liquids, out List<MaterialOrProductPowderConf>? powders)
        {
            items = null;
            if (this.Action is IExamineActionRequireItems erm)
            {
                items = new(1);
                erm.GetRequiredItems(this, items);
            }
            liquids = null;
            if (this.Action is IExamineActionRequireLiquid eapl)
            {
                liquids = new();
                eapl.GetRequiredLiquid(this, liquids);
            }
            powders = null;
            if (this.Action is IExamineActionRequirePowder eapp)
            {
                powders = new();
                eapp.GetRequiredPowder(this, powders);
            }
        }

        internal int GetAllMaterialCount()
        {
			GetAllMaterials(out var items, out var liquids, out var powders);
			return (items?.Count ?? 0) + (liquids?.Count ?? 0) + (powders?.Count ?? 0);
        }

        internal void GetAllProducts(out List<MaterialOrProductItemConf>? items, out List<MaterialOrProductLiquidConf>? liquids, out List<MaterialOrProductPowderConf>? powders)
        {
            items = null;
            if (this.Action is IExamineActionProduceItems erm)
            {
                items = new(1);
                erm.GetProducts(this, items);
            }
            liquids = null;
            if (this.Action is IExamineActionProduceLiquid eapl)
            {
                liquids = new();
                eapl.GetProductLiquid(this, liquids);
            }
            powders = null;
            if (this.Action is IExamineActionProducePowder eapp)
            {
                powders = new();
                eapp.GetProductPowder(this, powders);
            }
        }

        internal int GetAllProductCount()
        {
			GetAllProducts(out var items, out var liquids, out var powders);
			return (items?.Count ?? 0) + (liquids?.Count ?? 0) + (powders?.Count ?? 0);
        }

		public Dictionary<int, object> Temp { get; } = new Dictionary<int, object>();

        public GameObject GetObjectPointedAt ()
        {
            return GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(2);
        }
        public void SetBottomWarningMessage (string message) => Panel.SetBottomWarning(message);
	}
}
