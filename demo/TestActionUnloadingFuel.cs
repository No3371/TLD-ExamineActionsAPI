using ExamineActionsAPI;
using Il2Cpp;
using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace ExamineActionsAPIDemo
{
    class ActionUnloadingFuel : IExamineAction, IExamineActionProduceLiquid, IExamineActionDisplayInfo
    {
        public ActionUnloadingFuel()
        {
            info = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Loaded" },
                $"? L"
            );
        }

        public string Id => nameof(ActionUnloadingFuel);

        public string MenuItemLocalizationKey => "Unload";

        public string MenuItemSpriteName => null;

        public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Unload" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;
        InfoItemConfig info;

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.m_KeroseneLampItem != null;
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters.m_Units > 0;
        }
        void IExamineAction.OnPerforming(ExamineActionState state)
        {
            Il2CppTLD.Gear.KeroseneLampItem lamp = state.Subject.m_KeroseneLampItem;
            if (lamp.IsOn()) lamp.TurnOff(true);
        }

        public int CalculateDurationMinutes(ExamineActionState state) => 3;

        public float CalculateProgressSeconds(ExamineActionState state) => 2;
        public void OnSuccess(ExamineActionState state)
        {
            state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters = ItemLiquidVolume.FromLiters(0);
        }

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => false;
        void IExamineAction.OnActionSelected(ExamineActionState state) {}
        void IExamineAction.OnActionDeselected(ExamineActionState state) {}
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionProduceLiquid.GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
            liquids.Add(new (ExamineActionsAPI.PowderAndLiquidTypesLocator.KeroseneType, state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters.ToQuantity(1) * 0.95f, 100));
            liquids.Add(new (ExamineActionsAPI.PowderAndLiquidTypesLocator.KeroseneType, state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters.ToQuantity(1) * 0.05f, 50));
        }


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            info.Content = $"{state.Subject.m_KeroseneLampItem.m_CurrentFuelLiters}L";
            configs.Add(info);
        }
    }
}
