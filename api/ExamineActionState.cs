using Il2Cpp;
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
		internal bool SelectingTool { get; set; }
		public int SubActionId { get; internal set; }
		/// <summary>
		/// Time.realtimeSinceStartup
		/// </summary>
		public float? StartedRealtime { get; internal set; }
		public TLDDateTimeEAPI? StartedGameTime { get; internal set; }
		public void ResetActionMeta ()
		{
			ExamineActionsAPI.VeryVerboseLog($"ResetActionMeta");
			ActionInProgress = false;
			ActiveActionDurationMinutes = null;
			ActiveSuccessChance = null;
			ActiveResult = null;
			SelectedTool = null;
			InterruptionFlag = false;
			ActiveActionRequirementsMet = null;
			SelectingTool = false;
			Panel?.Toggle(false);
			Panel = null;
			Temp.Clear();
			SubActionId = 0;
			StartedRealtime = null;
			StartedGameTime = null;
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
            if (Action is IExamineActionRequireMaterials arm)
            {
                AllMaterialsReady = CheckMaterials(arm);
				if (!AllMaterialsReady.Value) ActiveActionRequirementsMet = false;
            }
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

        internal bool CheckMaterials (IExamineActionRequireMaterials act)
		{
			var hasAll = true;
			var mats = act.GetMaterials(this);
			if (mats == null) return true;
			for (int i = 0; i < mats.Length; i++)
			{
				int totalOfTheGearTypeToCheck = mats[i].Item2;
				for (int j = 0; j < mats.Length; j++)
					if (i != j && mats[i].Item1 == mats[j].Item1) totalOfTheGearTypeToCheck += mats[j].Item2;
				
				ExamineActionsAPI.VeryVerboseLog($"Checking for mat: {mats[i].Item1} x{totalOfTheGearTypeToCheck} (x{mats[i].Item2})");
				Il2CppSystem.Collections.Generic.List<GearItem> gears = new (1);
				GameManager.GetInventoryComponent().GearInInventory(mats[i].Item1, gears);
				for (int g = 0; g < gears.Count; g++)
				{
                    GearItem gearItem = gears[g];
                    if (gearItem == Subject) continue;

					totalOfTheGearTypeToCheck -= gearItem.m_StackableItem?.m_Units ?? 1;
					if (totalOfTheGearTypeToCheck <= 0) break;
				}
				// var invItem = GameManager.GetInventoryComponent().GearInInventory(mats[i].Item1, totalOfTheGearTypeToCheck);
				// if (invItem != null) continue; // Trying to get more units than owned would actually properly returns a null
				hasAll = totalOfTheGearTypeToCheck <= 0;
				break;
			}

			return hasAll;
		}

		public Dictionary<int, object> Temp { get; } = new Dictionary<int, object>();
	}
}
