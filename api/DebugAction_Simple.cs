using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    class DebugAction_Simple : IExamineAction
    {
        public DebugAction_Simple()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Cant";
        }

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public string Id => nameof(DebugAction_Simple);
        public string MenuItemLocalizationKey => "Cant";
        public string MenuItemSpriteName => "ico_lightSource_lantern";
        public LocalizedString ActionButtonLocalizedString { get; }

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return state.SubActionId == 1? 10 : 5;
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 20;
        }
        int IExamineAction.GetSubActionCounts(ExamineActionState state) => 2;

        public bool CanPerform(ExamineActionState state)
        {
            return state.SubActionId == 1;
        }

        public bool ConsumeOnCancel(ExamineActionState state)
        {
            return false;
        }

        public bool IsActionAvailable(GearItem gi)
        {
            return true;
        }

        public void OnActionDeselected(ExamineActionState state)
        {
        }

        public void OnActionSelected(ExamineActionState state)
        {
        }

        public void OnInterrupted(ExamineActionState state)
        {
        }

        public void OnPerform(ExamineActionState state)
        {
        }

        public void OnSuccess(ExamineActionState state)
        {
        }

        public bool ConsumeOnSuccess(ExamineActionState state) => false;
    }
}
