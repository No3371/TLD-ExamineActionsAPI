using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    class DebugAction_Materials : IExamineAction, IExamineActionRequireItems, IExamineActionFailable, IExamineActionCustomInfo
    {
        public DebugAction_Materials()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Materials";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "Materials");
			this.Info1.Title.m_LocalizationID = "Materials";
        }

        public string Id => nameof(DebugAction_Materials);
        public string MenuItemLocalizationKey => "Materials";
        public string MenuItemSpriteName => "ico_lightSource_lantern";

        public bool DisableDefaultPanel => false;

        public LocalizedString ActionButtonLocalizedString { get; }

        public InfoItemConfig? Info1 { get; }

        public InfoItemConfig? Info2 => null;

        public ActionsToBlock? LightRequirement => ActionsToBlock.Repair;

        public bool ConsumeOnFailure => false;

        public bool ConsumeMaterialsOnFailure => false;

        public bool AllowStarving => false;

        public bool AllowExhausted => false;

        public bool AllowFreezing => false;

        public bool AllowDehydrated => false;

        public bool AllowNonRiskAffliction => false;

        public float MinimumCondition => 0.5f;

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return 2;
        }


        public bool IsActionAvailable(GearItem gi)
        {
			return gi.m_FuelSourceItem != null;
        }
	
        public bool CanPerform(ExamineActionState state)
        {
			// return state.Subject.GearItemData.name.ToLower().Contains("meat");
			return false;
        }

        void IExamineActionRequireItems.GetRequiredItems(ExamineActionState state, System.Collections.Generic.List<(string gear_name, int units, byte chance)> items)
        {
            items.Add(("GEAR_Stick", 1, 100));
            items.Add(("GEAR_Stone", 2, 8));
            items.Add(("GEAR_Stone", 1, 55));
            items.Add(("GEAR_DustingSulfur", 1, 1));
        }


        public float CalculateFailureChance(ExamineActionState state)
        {
            return 95f;
        }

        public void OnActionFailed(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnSuccess(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnPerform(ExamineActionState state)
        {
			MelonLogger.Msg($"Performing Salt");
        }

        public void OnActionSelected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt selected");
        }

        public void OnActionDeselected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt deselected");
        }

        public InfoItemConfig? GetInfo1(ExamineActionState state) => Info1;

        public InfoItemConfig? GetInfo2(ExamineActionState state) => Info2;

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 6f;
        }

        public bool ConsumeOnCancel(ExamineActionState state)
        {
            return false;
        }

        bool IExamineActionFailable.ConsumeOnFailure(ExamineActionState state)
        {
            return true;
        }

        public bool ConsumeOnSuccess(ExamineActionState state)
        {
            return true;
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
