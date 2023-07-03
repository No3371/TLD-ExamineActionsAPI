using ExamineActionsAPI;
using Il2Cpp;

namespace ExamineActionsAPIDemo
{
    class TestActionBruteForceSharpening : IExamineAction, IExamineActionInterruptable, IExamineActionCancellable, IExamineActionCustomInfo
    {
        string IExamineAction.Id => nameof(TestActionBruteForceSharpening);

        string IExamineAction.MenuItemLocalizationKey => "Dumb Sharpen";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString => new LocalizedString() { m_LocalizationID = "Dumb Sharpen" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        ActionsToBlock? IExamineActionInterruptable.LightRequirementType => ActionsToBlock.Repair;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => true;

        float IExamineActionInterruptable.MinimumCondition => 0.5f;
        int IExamineAction.GetSubActionCounts(ExamineActionState state) => 5;
        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return 120 * (state.SubActionId + 1);
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 10 * (state.SubActionId + 1);
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.CurrentHP < state.Subject.GearItemData.MaxHP * 0.745f;
        }

        bool IExamineActionCancellable.ConsumeOnCancel(ExamineActionState state)
        {
            return false;
        }

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state)
        {
            return false;
        }

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            if (item.m_Sharpenable == null) return false;
            return true;
        }

        void IExamineActionCancellable.OnActionCanceled(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterrupted(ExamineActionState state) {}

        void IExamineAction.OnPerform(ExamineActionState state) {}

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            float normalizedNewCondition = state.Subject.GetNormalizedCondition() + 0.005f * (state.SubActionId + 1);
            if (normalizedNewCondition > 0.75f) normalizedNewCondition = 0.75f;
            state.Subject.SetNormalizedHP(normalizedNewCondition);
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo1(ExamineActionState state)
        {
            return new InfoItemConfig(new LocalizedString() { m_LocalizationID = "Max Condition" } , "75%");
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo2(ExamineActionState state) 
        {
            return new InfoItemConfig(new LocalizedString() { m_LocalizationID = "Improvement" } , $"{0.5f * (state.SubActionId + 1):0.00}%");
        }
    }
}
