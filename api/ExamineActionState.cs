using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    public class ExamineActionState
	{
        private GearItem? subject;
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
				ResetActionMeta();
				if (cache != value) ExamineActionsAPI.VeryVerboseLog($"Subject changed: {value?.name}");
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
		internal bool? ActiveActionRequirementsMet { get; set; }
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
			ActiveActionRequirementsMet = null;
			SelectingTool = false;
			Panel?.Toggle(false);
			Panel = null;
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
            ActiveActionRequirementsMet = true;
            MaybeUpdateSuccessChance();

			AllMaterialsReady = CheckMaterials(Action);
			if (!AllMaterialsReady.Value) ActiveActionRequirementsMet = false;

            if (Action is IExamineActionRequireTool eat)
            {
                var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
                pie.m_Tools.Clear();
                eat.GetToolOptions(this, pie.m_Tools);
                // for (int i = 0; i < pie.m_Tools.Count; i++)
                // 	ExamineActionsAPI.VeryVerboseLog($"{pie.m_Tools[i]?.name} ({pie.m_Tools[i].GetInstanceID()})");
                if (pie.m_Tools.Count == 0) // No tool
                {
                    ActiveActionRequirementsMet = false;
                }
            }
            ActiveActionDurationMinutes = Action.CalculateDurationMinutes(this);
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

        internal bool CheckMaterials (IExamineAction act)
		{
            List<(string gear_name, int units, byte chance)> materials = null;
            if (act is IExamineActionRequireItems erm)
            {
                materials = new (1);
                erm.GetRequiredItems(this, materials);
				for (int i = 0; i < materials.Count; i++)
				{
					int totalOfTheGearTypeToCheck = materials[i].Item2;
					for (int j = 0; j < materials.Count; j++)
						if (i != j && materials[i].Item1 == materials[j].Item1) totalOfTheGearTypeToCheck += materials[j].Item2;
					
					ExamineActionsAPI.VeryVerboseLog($"Checking for mat: {materials[i].Item1} x{totalOfTheGearTypeToCheck} (x{materials[i].Item2})");
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					GameManager.GetInventoryComponent().GearInInventory(materials[i].Item1, gears);
					for (int g = 0; g < gears.Count; g++)
					{
						GearItem gearItem = gears[g];
						if (gearItem == Subject) continue;

						totalOfTheGearTypeToCheck -= gearItem.m_StackableItem?.m_Units ?? 1;
						if (totalOfTheGearTypeToCheck <= 0) break;
					}
					if (totalOfTheGearTypeToCheck > 0) return false;
				}
            }
            List<(LiquidType type, float units, byte chance)> liquids = null;
            if (act is IExamineActionRequireLiquid erl)
            {
                liquids = new(1);
                erl.GetRequireLiquid(this, liquids);
				for (int i = 0; i < liquids.Count; i++)
				{
					float totalToCheck = liquids[i].units;
					for (int j = 0; j < liquids.Count; j++)
						if (i != j && liquids[i].Item1 == liquids[j].Item1) totalToCheck += liquids[j].units;
					
					ExamineActionsAPI.VeryVerboseLog($"Checking for liquid: {liquids[i].Item1} x{totalToCheck} (x{liquids[i].Item2})");
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					var totalInInv = GameManager.GetInventoryComponent().GetTotalLiquidVolume(liquids[i].type);
                    
					if (Subject.m_LiquidItem != null
                    && Subject.m_LiquidItem.m_LiquidType == liquids[i].type.LegacyLiquidType
                    && (int) Subject.m_LiquidItem.m_LiquidQuality == (int)liquids[i].type.Quality)
                        totalInInv -= Subject.m_LiquidItem.m_LiquidLiters;
					if (totalToCheck > totalInInv) return false;
					break;
				}
            }
            List<(PowderType type, float units, byte chance)> powders = null;
            if (act is IExamineActionRequirePowder erp)
            {
                powders = new(1);
                erp.GetRequiredPowder(this, powders);
				for (int i = 0; i < powders.Count; i++)
				{
					float totalToCheck = powders[i].units;
					for (int j = 0; j < i; j++)
						if (powders[i].type == powders[j].type) totalToCheck += powders[j].units;
					
					Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
					var totalInInv = GameManager.GetInventoryComponent().GetTotalPowderWeight(powders[i].type);
					ExamineActionsAPI.VeryVerboseLog($"Checking for powder: {powders[i].Item1} x{totalToCheck} (x{powders[i].Item2}) (has: {totalInInv})");
					if (Subject.m_PowderItem?.m_Type == powders[i].type) totalInInv -= Subject.m_PowderItem.m_WeightKG;
					if (totalToCheck > totalInInv) return false;
					break;
				}
            }

			return true;
		}


        internal void GetAllMaterials(out List<(string gear_name, int units, byte chance)>? items, out List<(LiquidType type, float units, byte chance)>? liquids, out List<(PowderType type, float units, byte chance)>? powders)
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
                eapl.GetRequireLiquid(this, liquids);
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

        internal void GetAllProducts(out List<(string gear_name, int units, byte chance)>? items, out List<(LiquidType type, float units, byte chance)>? liquids, out List<(PowderType type, float units, byte chance)>? powders)
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
	}
}
