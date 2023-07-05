using Il2Cpp;

namespace ExamineActionsAPI
{
    public abstract class SimpleActionTemplate : IExamineAction
    {
        public abstract string Id { get; }
        public abstract string MenuItemLocalizationKey { get; }
        public virtual string? MenuItemSpriteName { get; } = null;
        public abstract LocalizedString ActionButtonLocalizedString { get; }
        public virtual IExamineActionPanel? CustomPanel { get; } = null;
        public abstract int CalculateDurationMinutes(ExamineActionState state);
        public abstract float CalculateProgressSeconds(ExamineActionState state);
        public abstract bool CanPerform(ExamineActionState state);
        public abstract bool ConsumeOnSuccess(ExamineActionState state);
        public abstract bool IsActionAvailable(GearItem item);
        public virtual void OnActionDeselected(ExamineActionState state) {}
        public virtual void OnActionInterruptedBySystem(ExamineActionState state) {}
        public virtual void OnActionSelected(ExamineActionState state) {}
        public virtual void OnPerform(ExamineActionState state) {}
        public abstract void OnSuccess(ExamineActionState state);
    }
}
