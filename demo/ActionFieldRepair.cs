using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionFieldRepair : IExamineAction, IExamineActionInterruptable, IExamineActionCancellable, IExamineActionCustomInfo, IExamineActionRequireItems, IExamineActionFailable
    {
        string IExamineAction.Id => nameof(ActionFieldRepair);

        string IExamineAction.MenuItemLocalizationKey => "Field Repair";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString => new LocalizedString() { m_LocalizationID = "Repair" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        ActionsToBlock? IExamineActionInterruptable.LightRequirementType => ActionsToBlock.Repair;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => true;

        float IExamineActionInterruptable.MinimumCondition => 0.5f;

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return Mathf.Max(5, state.Subject.m_Repairable.m_DurationMinutes/10);
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 3;
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.GetNormalizedCondition() < GetNormalizedConditionCap(state);
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
            if (item.m_ClothingItem == null || item.m_Repairable == null || item.CurrentHP == 0) return false;
            return true;
        }

        void IExamineActionCancellable.OnActionCanceled(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterrupted(ExamineActionState state) {}

        void IExamineAction.OnPerform(ExamineActionState state) {}

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            state.Subject.SetNormalizedHP(UnityEngine.Random.Range(state.Subject.GetNormalizedCondition(), GetNormalizedConditionCap(state)));
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo1(ExamineActionState state)
        {
            var conf = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Repair Result" },
                $"{state.Subject.GetNormalizedCondition()*100f:0.0}% ~ {GetNormalizedConditionCap(state)*100f:0.0}%"
            );
            if (state.Subject.GetNormalizedCondition() > GetNormalizedConditionCap(state))
            {
                conf.Content = "N/A";
            }
            return conf;
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo2(ExamineActionState state) 
        {
            var conf = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Condition Cap" },
                $"{GetNormalizedConditionCap(state)*100f:0.0}%"
            );if (state.Subject.GetNormalizedCondition() > GetNormalizedConditionCap(state))
            {
                conf.LabelColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
            }
            return conf;
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionRequireItems.GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
            items.Add(new ("GEAR_GutDried", 1, 100));
            items.Add(new ("GEAR_GutDried", 1, 50));
            items.Add(new ("GEAR_Cloth", 1, 100));
        }

        float IExamineActionFailable.CalculateFailureChance(ExamineActionState state)
        {
            return 10;
        }

        void IExamineActionFailable.OnActionFailed(ExamineActionState state) {}

        bool IExamineActionFailable.ConsumeOnFailure(ExamineActionState state)
        {
            return false;
        }

        float GetNormalizedConditionCap (ExamineActionState state) => state.Subject.m_Repairable.m_RepairConditionCap / 100f / 5f;
    }
}
