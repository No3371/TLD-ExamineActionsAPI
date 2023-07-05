using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class TestActionPiling : IExamineAction, IExamineActionProduceItems
    {
        public TestActionPiling() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(TestActionPiling);

        string IExamineAction.MenuItemLocalizationKey => "Pile";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Pile" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name switch
            {
                "GEAR_Stick" => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_Stone" => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_Coal" => true && item.m_StackableItem.m_Units >= 4,
                "GEAR_Charcoal" => true && item.m_StackableItem.m_Units >= 10,
                "GEAR_CattailStalk" => true && item.m_StackableItem.m_Units >= 5,
                _ => false
            };
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.name switch
            {
                "GEAR_Stick" => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_Stone" => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_Coal" => state.Subject.m_StackableItem.m_Units >= 4 * (state.SubActionId + 1),
                "GEAR_Charcoal" => state.Subject.m_StackableItem.m_Units >= 10 * (state.SubActionId + 1),
                "GEAR_CattailStalk" => state.Subject.m_StackableItem.m_Units >= 5 * (state.SubActionId + 1),
                _ => false
            };
        }
        void IExamineAction.OnPerform(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return (state.SubActionId + 1);
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return Mathf.Max(1, (state.SubActionId + 1) * 0.5f);
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;
        int IExamineAction.OverrideConsumingUnits(ExamineActionState state)
        {
            return state.Subject.name switch
            {
                "GEAR_Stick" => (state.SubActionId + 1) * 10,
                "GEAR_Stone" => (state.SubActionId + 1) * 10,
                "GEAR_Coal" => (state.SubActionId + 1) * 4,
                "GEAR_Charcoal" => (state.SubActionId + 1) * 10,
                "GEAR_CattailStalk" => (state.SubActionId + 1) * 5,
                _ => 1
            };
        }

        int IExamineAction.GetSubActionCounts(ExamineActionState state)
        {
            return state.Subject.name switch
            {
                "GEAR_Stick" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_Stone" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_Coal" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 4, 1, 5),
                "GEAR_Charcoal" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 10, 1, 5),
                "GEAR_CattailStalk" => Mathf.Clamp(state.Subject.m_StackableItem.m_Units / 5, 1, 5),
                _ => 1
            };
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, List<(string gear_name, int units, byte chance)> products)
        {
            products.Add(state.Subject.name switch
            {
                "GEAR_Stick" => state.SubActionId switch
                {
                    4 => ("GEAR_StickPile050", 1, 100),
                    _ => ("GEAR_StickPile010", state.SubActionId + 1, 100)
                },
                "GEAR_Stone" => ("GEAR_StonePile010", state.SubActionId + 1, 100),
                "GEAR_Coal" => ("GEAR_CoalPile004", state.SubActionId + 1, 100),
                "GEAR_Charcoal" => ("GEAR_CharcoalPile010", state.SubActionId + 1, 100),
                "GEAR_CattailStalk" => ("GEAR_CattailPile005", state.SubActionId + 1, 100),
                _ => ("", 0, 0)
            });
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
