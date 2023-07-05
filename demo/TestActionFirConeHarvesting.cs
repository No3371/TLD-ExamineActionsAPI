using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    // 1FirConeFuel
    // 3FirConeSeeds *2
    // 4FirCone
    // 10min
    class TestActionFirConeHarvesting : IExamineAction, IExamineActionProduceItems
    {
        public TestActionFirConeHarvesting() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(TestActionFirConeHarvesting);

        string IExamineAction.MenuItemLocalizationKey => "Harvest";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Harvest" };

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

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<(string gear_name, int units, byte chance)> products)
        {
            products.Add(("GEAR_1FirConeFuel", 1 * (state.SubActionId + 1), 100));
            products.Add(("GEAR_3FirConeSeeds", 2 * (state.SubActionId + 1), 100));
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
