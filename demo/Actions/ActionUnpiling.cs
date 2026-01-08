using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionUnpiling : IExamineAction, IExamineActionProduceItems, IExamineActionHasDependendency
    {
        public ActionUnpiling() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(ActionUnpiling);

        string IExamineAction.MenuItemLocalizationKey => "Unpile";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Unpile" };
        public string[]? MelonDependency { get; set; }
        public string[]? GearNameDependency { get; set; } = { "GEAR_StickPile010" };
        public string[]? CSharpTypeDependency { get; set; }

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name switch
            {
                "GEAR_StickPile050F" => true,
                "GEAR_StickPile050" => true,
                "GEAR_StickPile010" => true,
                "GEAR_StonePile010" => true,
                "GEAR_CoalPile004" => true,
                "GEAR_CharcoalPile010" => true,
                "GEAR_CattailPile005" => true,
                "GEAR_SoftwoodPile005" => true,
                "GEAR_HardwoodPile005" => true,
                "GEAR_ScrapPile010" => true,
                "GEAR_RwoodPile010" => true,
                _ => false
            };
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return true;
        }
        void IExamineAction.OnPerforming(ExamineActionState state) {}

        int IExamineAction.GetDurationMinutes(ExamineActionState state)
        {
            return (state.SubActionId + 1);
        }

        float IExamineAction.GetProgressSeconds(ExamineActionState state)
        {
            return Mathf.Max(1, (state.SubActionId + 1) * 0.5f);
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => true;
        int IExamineAction.GetConsumingUnits(ExamineActionState state)
        {
            return state.SubActionId + 1;
        }

        int IExamineAction.GetSubActionCount(ExamineActionState state)
        {
            return state.Subject.m_StackableItem.m_Units;
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(state.Subject.name switch
            {
                "GEAR_StickPile050F" => new ("GEAR_Stick", 50 * (state.SubActionId + 1), 100),
                "GEAR_StickPile050" => new ("GEAR_Stick", 50 * (state.SubActionId + 1), 100),
                "GEAR_StickPile010" => new ("GEAR_Stick", 10 * (state.SubActionId + 1), 100),
                "GEAR_StonePile010" => new ("GEAR_Stone", 10 * (state.SubActionId + 1), 100),
                "GEAR_CoalPile004" => new ("GEAR_Coal", 4 * (state.SubActionId + 1), 100),
                "GEAR_CharcoalPile010" => new ("GEAR_Charcoal", 10 * (state.SubActionId + 1), 100),
                "GEAR_CattailPile005" => new ("GEAR_CattailStalk", 5 * (state.SubActionId + 1), 100),
                "GEAR_SoftwoodPile005" => new ("GEAR_Softwood", 5 * (state.SubActionId + 1), 100),
                "GEAR_HardwoodPile005" => new ("GEAR_Hardwood", 5 * (state.SubActionId + 1), 100),
                "GEAR_ScrapPile010" => new ("GEAR_ScrapMetal", 10 * (state.SubActionId + 1), 100),
                "GEAR_RwoodPile010" => new ("GEAR_ReclaimedWoodB", 10 * (state.SubActionId + 1), 100),
                _ => new ("", 0, 0)
            });
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
