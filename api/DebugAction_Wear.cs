using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    class DebugAction_Wear : SimpleActionTemplate
    {
        public DebugAction_Wear()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Wear";
        }

        public override string Id => nameof(DebugAction_Wear);
        public override string MenuItemLocalizationKey => "Wear";
        public override string MenuItemSpriteName => "ico_lightSource_lantern";
        public override LocalizedString ActionButtonLocalizedString { get; }

        public override int CalculateDurationMinutes(ExamineActionState state)
        {
            return 1;
        }

        public override float CalculateProgressSeconds(ExamineActionState state)
        {
            return 0.5f;
        }

        public override bool CanPerform(ExamineActionState state)
        {
            return state.Subject.CurrentHP > 0;
        }

        public override bool IsActionAvailable(GearItem gi)
        {
            return gi.m_Repairable != null;
        }

        public override void OnSuccess(ExamineActionState state)
        {
            float normalizedNewCondition = state.Subject.GetNormalizedCondition() / 2f;
            if (normalizedNewCondition < 0.1f) normalizedNewCondition = 0;
            state.Subject.SetNormalizedHP(normalizedNewCondition);
        }
        public override bool ShouldConsumeOnSuccess(ExamineActionState state) => false;
    }
}
