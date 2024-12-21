using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    class DebugAction_Simple : SimpleActionTemplate
    {
        public DebugAction_Simple()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Cant";
        }

        public override string Id => nameof(DebugAction_Simple);
        public override string MenuItemLocalizationKey => "Cant";
        public override string MenuItemSpriteName => "ico_lightSource_lantern";
        public override LocalizedString ActionButtonLocalizedString { get; }

        public override int CalculateDurationMinutes(ExamineActionState state)
        {
            return state.SubActionId == 1? 10 : 5;
        }

        public override float CalculateProgressSeconds(ExamineActionState state)
        {
            return 20;
        }

        public override bool CanPerform(ExamineActionState state)
        {
            return state.SubActionId == 1;
        }

        public override bool IsActionAvailable(GearItem gi)
        {
            return true;
        }

        public override void OnSuccess(ExamineActionState state) {}
        public override bool ShouldConsumeOnSuccess(ExamineActionState state) => false;
    }
}
