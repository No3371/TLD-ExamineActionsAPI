using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    /// <summary>
    /// This action provides an easy way to harvest fir cones from Bountiful Foraging mod.
    /// </summary>
    class ActionFirConeHarvesting : IExamineAction, IExamineActionProduceItems, IExamineActionInterruptable
    {
        public ActionFirConeHarvesting() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(ActionFirConeHarvesting);

        string IExamineAction.MenuItemLocalizationKey => "Harvest";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Harvest" };

        ActionsToBlock? IExamineActionInterruptable.LightRequirementType => null;

        bool IExamineActionInterruptable.InterruptOnStarving => false;

        bool IExamineActionInterruptable.InterruptOnExhausted => false;

        bool IExamineActionInterruptable.InterruptOnFreezing => false;

        bool IExamineActionInterruptable.InterruptOnDehydrated => false;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => false;

        float IExamineActionInterruptable.NormalizedConditionInterruptThreshold => 0f;

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

        int IExamineAction.GetSubActionCount(ExamineActionState state) => state.Subject.m_StackableItem.m_Units;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            int times = state.SubActionId + 1;
            products.Add(new ("GEAR_1FirConeFuel", 1 * times, 100));
            if (times % 5 == 0)
                products.Add(new ("GEAR_2FirSeedBunch", 2 * times / 5, 100));
            else 
                products.Add(new ("GEAR_3FirConeSeeds", 2 * times, 100));
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state)
        {
            (this as IExamineActionInterruptable)?.OnInterrupted(state);
        }

        void IExamineActionInterruptable.OnInterrupted(ExamineActionState state)
        {
            int completed = (int) (state.NormalizedProgress!.Value * state.SubActionId);
            GameManager.m_PlayerManager.InstantiateItemInPlayerInventory(GearItem.LoadGearItemPrefab("GEAR_1FirConeFuel"), completed);
            GameManager.m_PlayerManager.InstantiateItemInPlayerInventory(GearItem.LoadGearItemPrefab("GEAR_3FirConeSeeds"), completed * 2);
            state.Subject!.m_StackableItem.m_Units -= completed;
        }
    }
}
