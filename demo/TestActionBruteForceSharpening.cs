using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionBruteForceSharpening : IExamineAction, IExamineActionInterruptable, IExamineActionCancellable, IExamineActionCustomInfo
    {
        string IExamineAction.Id => nameof(ActionBruteForceSharpening);

        string IExamineAction.MenuItemLocalizationKey => "Brute Force Sharpen";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString => new LocalizedString() { m_LocalizationID = "Sharpen" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        // Because the action is designed to take quite some time to finish
        // We implements IExamineActionInterruptable so the action will be automatically stopped when:
        // - The player has health issue
        // - The player does not have enough lighting (knives are dangerous!)
        ActionsToBlock? IExamineActionInterruptable.LightRequirementType => ActionsToBlock.Repair;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => true;

        float IExamineActionInterruptable.MinimumCondition => 0.5f;

        // For this action, we are using SubActions as a scaling factor of how much condition to sharpen
        // I picked 5 because players don't really spend more hours at once
        int IExamineAction.GetSubActionCount(ExamineActionState state) => 5;
        // The base ingame time to sharpen is 2 hours, and we scale it by SubActionId
        // SubActionid is 0 based so we add 1 to it
        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return 120 * (state.SubActionId + 1);
        }

        // The base time to sharpen is 4 seconds, and we scale it by SubActionId
        // it's to prevent players performing this unaware of how much time it
        // But we limit it to 12 seconds because 10+ seconds already always feels long
        // SubActionid is 0 based so we add 1 to it
        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return Mathf.Min(12, 4 * (state.SubActionId + 1));
        }

        // This action cost nothing but time so to balance it we limit it to sharpen stuff up to 75%
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

        // Here we modify the condition of the item being sharpened
        // Normalized means 0.0 ~ 1.0 (0% ~ 100%)
        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            float normalizedNewCondition = state.Subject.GetNormalizedCondition() + 0.005f * (state.SubActionId + 1);
            if (normalizedNewCondition > 0.75f) normalizedNewCondition = 0.75f;
            state.Subject.SetNormalizedHP(normalizedNewCondition);
        }

        // Display the 75% limit
        InfoItemConfig? IExamineActionCustomInfo.GetInfo1(ExamineActionState state)
        {
            return new InfoItemConfig(new LocalizedString() { m_LocalizationID = "Max Condition" } , "75%");
        }

        // Display how much will the condition will be improved
        InfoItemConfig? IExamineActionCustomInfo.GetInfo2(ExamineActionState state) 
        {
            return new InfoItemConfig(new LocalizedString() { m_LocalizationID = "Improvement" } , $"{0.5f * (state.SubActionId + 1):0.00}%");
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
