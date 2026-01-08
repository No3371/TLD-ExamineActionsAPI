using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionPiling : IExamineAction, IExamineActionProduceItems, IExamineActionHasDependendency
    {
        public ActionPiling() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(ActionPiling);

        string IExamineAction.MenuItemLocalizationKey => "Pile";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Pile" };
        public string[]? MelonDependency { get; set; }
        public string[]? GearNameDependency { get; set; } = { "GEAR_StickPile010" };
        public string[]? CSharpTypeDependency { get; set; }

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name switch
            {
                "GEAR_Stick"          => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_Stone"          => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_Coal"           => true && item.m_StackableItem.m_Units >= 4,
                "GEAR_Charcoal"       => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_CattailStalk"   => true && item.m_StackableItem.m_Units >= 5,
                "GEAR_Softwood"       => true && item.m_StackableItem.m_Units >= 5,
                "GEAR_Hardwood"       => true && item.m_StackableItem.m_Units >= 5,
                "GEAR_ScrapMetal"     => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_ReclaimedWoodB" => true && item.m_StackableItem.m_Units >= 10,
                _ => false
            };
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.name switch
            {
                "GEAR_Stick"          => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_Stone"          => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_Coal"           => state.Subject.m_StackableItem.m_Units >= 4 * (state.SubActionId + 1),
                "GEAR_Charcoal"       => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_CattailStalk"   => state.Subject.m_StackableItem.m_Units >= 5 * (state.SubActionId + 1),
                "GEAR_Softwood"       => true && state.Subject.m_StackableItem.m_Units >= 5 * (state.SubActionId + 1),
                "GEAR_Hardwood"       => true && state.Subject.m_StackableItem.m_Units >= 5 * (state.SubActionId + 1),
                "GEAR_ScrapMetal"     => true && state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_ReclaimedWoodB" => true && state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                _ => false
            };
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
            return state.Subject.name switch
            {
                "GEAR_Stick"          => (state.SubActionId + 1) * 10,
                "GEAR_Stone"          => (state.SubActionId + 1) * 10,
                "GEAR_Coal"           => (state.SubActionId + 1) * 4,
                "GEAR_Charcoal"       => (state.SubActionId + 1) * 10,
                "GEAR_CattailStalk"   => (state.SubActionId + 1) * 5,
                "GEAR_Softwood"       => (state.SubActionId + 1) * 5,
                "GEAR_Hardwood"       => (state.SubActionId + 1) * 5,
                "GEAR_ScrapMetal"     => (state.SubActionId + 1) * 10,
                "GEAR_ReclaimedWoodB" => (state.SubActionId + 1) * 10,
                _ => 1
            };
        }

        int IExamineAction.GetSubActionCount(ExamineActionState state)
        {
            return state.Subject.name switch
            {
                "GEAR_Stick"          => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_Stone"          => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_Coal"           => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 4, 1, 5),
                "GEAR_Charcoal"       => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_CattailStalk"   => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 5, 1, 5),
                "GEAR_Softwood"       => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 5, 1, 5),
                "GEAR_Hardwood"       => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 5, 1, 5),
                "GEAR_ScrapMetal"     => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_ReclaimedWoodB" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                _ => 1
            };
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(state.Subject.name switch
            {
                "GEAR_Stick" => state.SubActionId switch
                {
                    4 => new ("GEAR_StickPile050", 1, 100),
                    _ => new ("GEAR_StickPile010", state.SubActionId + 1, 100)
                },
                "GEAR_Stone" => new ("GEAR_StonePile010", state.SubActionId + 1, 100),
                "GEAR_Coal" => new ("GEAR_CoalPile004", state.SubActionId + 1, 100),
                "GEAR_Charcoal" => new ("GEAR_CharcoalPile010", state.SubActionId + 1, 100),
                "GEAR_CattailStalk" => new ("GEAR_CattailPile005", state.SubActionId + 1, 100),
                "GEAR_Softwood" => new ("GEAR_SoftwoodPile005", state.SubActionId + 1, 100),
                "GEAR_Hardwood" => new ("GEAR_HardwoodPile005", state.SubActionId + 1, 100),
                "GEAR_ScrapMetal" => new ("GEAR_ScrapPile010", state.SubActionId + 1, 100),
                "GEAR_ReclaimedWoodB" => new ("GEAR_RwoodPile010", state.SubActionId + 1, 100),
                _ => new ("", 0, 0)
            });
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
