using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    public class ActionFirSeedBunch : SimpleActionTemplate, IExamineAction, IExamineActionProduceItems
    {
        public override string Id => nameof(ActionFirSeedBunch);

        public override string MenuItemLocalizationKey => "Pile";

        public override LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Pile" };

        public override int CalculateDurationMinutes(ExamineActionState state) => 1;

        public override float CalculateProgressSeconds(ExamineActionState state) => 1;

        public override bool CanPerform(ExamineActionState state) => state.Subject.m_StackableItem.m_Units >= 5;

        public override bool ConsumeOnSuccess(ExamineActionState state) => true;
        public override int OverrideConsumingUnits (ExamineActionState state) => (state.SubActionId + 1) * 5;
        public override int GetSubActionCount (ExamineActionState state) => state.Subject.m_StackableItem.m_Units / 5;

        public override bool IsActionAvailable(GearItem item)
        {
            return item.name == "GEAR_3FirConeSeeds" || item.name == "GEAR_2FirConeSeedsRoasted";
        }

        public override void OnSuccess(ExamineActionState state) {}

        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new (state.Subject.name == "GEAR_2FirConeSeedsRoasted" ?  "GEAR_1FirSeedBunchRoasted" :  "GEAR_2FirSeedBunch", (state.SubActionId + 1), 100));
        }
    }
}
