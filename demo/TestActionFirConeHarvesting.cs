using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    /// <summary>
    /// This action provides an easy way to harvest fir cones from Bountiful Foraging mod.
    /// </summary>
    class TestActionFirConeHarvesting : IExamineAction, IExamineActionProduceItems, IExamineActionInterruptable
    {
        public TestActionFirConeHarvesting() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(TestActionFirConeHarvesting);

        string IExamineAction.MenuItemLocalizationKey => "Harvest";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Harvest" };

        ActionsToBlock? IExamineActionInterruptable.LightRequirementType => null;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => true;

        float IExamineActionInterruptable.MinimumCondition => 0.5f;

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name == "GEAR_4FirCone";
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state) => true;
        void IExamineAction.OnPerform(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return (state.SubActionId + 1) * 10;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return (state.SubActionId + 1) * 1;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;
        int IExamineAction.OverrideConsumingUnits(ExamineActionState state) => (state.SubActionId + 1);

        int IExamineAction.GetSubActionCounts(ExamineActionState state) => state.Subject.m_StackableItem.m_Units;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_1FirConeFuel", 1 * (state.SubActionId + 1), 100));
            products.Add(new ("GEAR_3FirConeSeeds", 2 * (state.SubActionId + 1), 100));
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterrupted(ExamineActionState state)
        {
            throw new NotImplementedException();
        }
    }
}
