using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    class DebugAction_Products : IExamineAction, IExamineActionProduceItems, IExamineActionFailable
    {
        public DebugAction_Products()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Products";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "Products");
			this.Info1.Title.m_LocalizationID = "Test Title2";
        }

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public string Id => nameof(DebugAction_Products);
        public string MenuItemLocalizationKey => "Products";
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

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return 12;
        }


        public bool IsActionAvailable(GearItem gi)
        {
			return gi.m_FuelSourceItem != null;
        }
	
        public bool CanPerform(ExamineActionState state)
        {
			return true;
        }

        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_Knife", 1, 100));
            products.Add(new ("GEAR_WhiskyFlask", 1, 8));
        }


        public float CalculateFailureChance(ExamineActionState state)
        {
            return 5f;
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

        public InfoItemConfig? GetInfo1() => Info1;

        public InfoItemConfig? GetInfo2() => Info2;

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 3f;
        }

        public bool ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool ConsumeMaterialsOnInterruption(ExamineActionState state, string matGearName)
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
