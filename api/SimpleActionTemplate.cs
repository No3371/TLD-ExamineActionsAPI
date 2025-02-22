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
        public abstract int GetDurationMinutes(ExamineActionState state);
        public abstract float GetProgressSeconds(ExamineActionState state);
        public abstract bool CanPerform(ExamineActionState state);
        public abstract bool ShouldConsumeOnSuccess(ExamineActionState state);
        public abstract bool IsActionAvailable(GearItem item);
        public virtual void OnActionDeselected(ExamineActionState state) {}
        public virtual void OnActionInterruptedBySystem(ExamineActionState state) {}
        public virtual void OnActionSelected(ExamineActionState state) {}
        public virtual void OnPerforming(ExamineActionState state) {}
        public abstract void OnSuccess(ExamineActionState state);
        public virtual int GetSubActionCount(ExamineActionState state) => 1;
        public virtual int GetConsumingUnits(ExamineActionState state) => 1;
    }
}
