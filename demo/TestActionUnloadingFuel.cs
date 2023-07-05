using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    class TestActionUnloadingFuel : SimpleActionTemplate, IExamineActionProduceItems
    {
        public TestActionUnloadingFuel() {}

        public override string Id => nameof(TestActionUnloadingFuel);

        public override string MenuItemLocalizationKey => "Unload";

        public override string MenuItemSpriteName => null;

        public override LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Unload" };

        public override bool IsActionAvailable(GearItem item)
        {
            return item.m_KeroseneLampItem != null;
            
        }

        public override bool CanPerform(ExamineActionState state)
        {
            return state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters > 0;
        }
        public override void OnPerform(ExamineActionState state)
        {
            Il2CppTLD.Gear.KeroseneLampItem lamp = state.Subject.m_KeroseneLampItem;
            if (lamp.IsOn()) lamp.TurnOff(true);

            state.Temp[0] = state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters;
        }

        public override int CalculateDurationMinutes(ExamineActionState state) => 3;

        public override float CalculateProgressSeconds(ExamineActionState state) => 2;
        public override void OnSuccess(ExamineActionState state)
        {
            state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters = 0;
        }

        public override bool ConsumeOnSuccess(ExamineActionState state) => false;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_LampFuel", 1, 100) );
        }

        void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, Il2Cpp.GearItem product)
        {
            product.m_LiquidItem.m_LiquidLiters = (float) state.Temp[0];
        }
    }
}
