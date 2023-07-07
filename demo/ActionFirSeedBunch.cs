using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    public class ActionFirSeedBunch : IExamineAction, IExamineActionProduceItems
    {
        public string Id => nameof(ActionFirSeedBunch);

        public string MenuItemLocalizationKey => "Pile";

        public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Pile" };

        string? IExamineAction.MenuItemSpriteName => null;

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public int CalculateDurationMinutes(ExamineActionState state) => 1;

        public float CalculateProgressSeconds(ExamineActionState state) => 1;

        public bool CanPerform(ExamineActionState state) => state.Subject.m_StackableItem.m_Units >= 5;

        public bool ConsumeOnSuccess(ExamineActionState state) => true;
        public int OverrideConsumingUnits (ExamineActionState state) => (state.SubActionId + 1) * 5;
        int IExamineAction.GetSubActionCount (ExamineActionState state) => state.Subject.m_StackableItem.m_Units / 5;

        public bool IsActionAvailable(GearItem item)
        {
            return (item.name == "GEAR_3FirConeSeeds" || item.name == "GEAR_2FirConeSeedsRoasted") && (item?.m_StackableItem?.m_Units ?? 1) >= 5;
        }

        public void OnSuccess(ExamineActionState state) {}

        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new (state.Subject.name == "GEAR_2FirConeSeedsRoasted" ?  "GEAR_1FirSeedBunchRoasted" :  "GEAR_2FirSeedBunch", (state.SubActionId + 1), 100));
        }

        void IExamineAction.OnPerform(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
