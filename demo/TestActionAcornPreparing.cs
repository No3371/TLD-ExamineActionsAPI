using ExamineActionsAPI;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{

    class TestActionAcornPreparing : IExamineAction, IExamineActionProduceItems
    {
        public TestActionAcornPreparing() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(TestActionAcornPreparing);

        string IExamineAction.MenuItemLocalizationKey => "Prepare";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Prepare" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name == "GEAR_Acorn";
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state) => true;
        void IExamineAction.OnPerform(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return (state.SubActionId + 1) * 10;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return state.SubActionId == 0? 1 : 2;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;
        int IExamineAction.OverrideConsumingUnits(ExamineActionState state) => state.SubActionId + 1;
        int IExamineAction.GetSubActionCounts(ExamineActionState state) => Mathf.Min(4, state.Subject.m_StackableItem?.m_Units ?? 1);

        (string, int, byte)[] IExamineActionProduceItems.GetProducts(ExamineActionState state)
        {
            return state.SubActionId switch
            {
                0 => new (string, int, byte)[]{ ("GEAR_AcornShelled", 1, 100) },
                1 => new (string, int, byte)[]{ ("GEAR_AcornShelled", 2, 100) },
                2 => new (string, int, byte)[]{ ("GEAR_AcornShelled", 3, 100) },
                3 => new (string, int, byte)[]{ ("GEAR_AcornShelledBig", 1, 100) }
            };
        }
    }
}
