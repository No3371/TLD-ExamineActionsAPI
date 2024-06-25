using ExamineActionsAPI;
using Il2Cpp;
using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace ExamineActionsAPIDemo
{
    class ActionUnloadingFuel : IExamineAction, IExamineActionProduceLiquid
    {
        public ActionUnloadingFuel() {}

        public string Id => nameof(ActionUnloadingFuel);

        public string MenuItemLocalizationKey => "Unload";

        public string MenuItemSpriteName => null;

        public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Unload" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public bool IsActionAvailable(GearItem item)
        {
            return item.m_KeroseneLampItem != null;
            
        }

        public bool CanPerform(ExamineActionState state)
        {
            return state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters.m_Units > 0;
        }
        public void OnPerform(ExamineActionState state)
        {
            Il2CppTLD.Gear.KeroseneLampItem lamp = state.Subject.m_KeroseneLampItem;
            if (lamp.IsOn()) lamp.TurnOff(true);

            state.Temp[0] = state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters;
        }

        public int CalculateDurationMinutes(ExamineActionState state) => 3;

        public float CalculateProgressSeconds(ExamineActionState state) => 2;
        public void OnSuccess(ExamineActionState state)
        {
            state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters = ItemLiquidVolume.FromLiters(0);
        }

        public bool ConsumeOnSuccess(ExamineActionState state) => false;

        // void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        // {
        //     products.Add(new ("GEAR_LampFuel", 1, 100));
        // }
        // void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, Il2Cpp.GearItem product)
        // {
        //     product.m_LiquidItem.m_LiquidLiters = (float) state.Temp[0];
        // }

        void IExamineAction.OnActionSelected(ExamineActionState state) {}
        void IExamineAction.OnActionDeselected(ExamineActionState state) {}
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionProduceLiquid.GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
            liquids.Add(new (ExamineActionsAPI.PowderAndLiquidTypesLocator.KeroseneType, state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters.ToQuantity(LiquidType.m_Kerosene.Density), 100));
        }
    }
}
