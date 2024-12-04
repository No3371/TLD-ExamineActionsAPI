using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    // Items that can be harvested does not disappear even when ruined
    class ActionDisposingRuined : IExamineAction
    {
        public ActionDisposingRuined() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(ActionDisposingRuined);

        string IExamineAction.MenuItemLocalizationKey => "Dispose (Destroy)";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Dispose" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.IsWornOut();
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return true;
        }
        void IExamineAction.OnPerform(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return 1;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 1;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
