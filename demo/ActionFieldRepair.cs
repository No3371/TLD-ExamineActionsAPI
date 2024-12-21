using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionFieldRepair : IExamineAction, IExamineActionInterruptable, IExamineActionCancellable, IExamineActionDisplayInfo, IExamineActionRequireItems, IExamineActionFailable
    {
        string IExamineAction.Id => nameof(ActionFieldRepair);

        string IExamineAction.MenuItemLocalizationKey => "Field Repair";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString => new LocalizedString() { m_LocalizationID = "Repair" };

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => true;

        float IExamineActionInterruptable.NormalizedConditionInterruptThreshold => 0.5f;

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

        bool IExamineActionCancellable.ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return false;
        }

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            if (item.m_ClothingItem == null || item.m_Repairable == null || item.CurrentHP == 0) return false;
            return true;
        }

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterruption(ExamineActionState state) {}
        ActionsToBlock? IExamineActionInterruptable.GetLightRequirementType(ExamineActionState state) => ActionsToBlock.Repair;

        void IExamineAction.OnPerforming(ExamineActionState state) {}

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            state.Subject.SetNormalizedHP(UnityEngine.Random.Range(state.Subject.GetNormalizedCondition(), GetNormalizedConditionCap(state)));
        }


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            var conf = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Repair Result" },
                $"{state.Subject.GetNormalizedCondition()*100f:0.0}% ~ {GetNormalizedConditionCap(state)*100f:0.0}%"
            );
            if (state.Subject.GetNormalizedCondition() > GetNormalizedConditionCap(state))
            {
                conf.Content = "N/A";
            }

            configs.Add(conf);
            
            conf = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Condition Cap" },
                $"{GetNormalizedConditionCap(state)*100f:0.0}%"
            );if (state.Subject.GetNormalizedCondition() > GetNormalizedConditionCap(state))
            {
                conf.LabelColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
            }
            configs.Add(conf);
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionRequireItems.GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
            items.Add(new ("GEAR_GutDried", 1, 100));
            items.Add(new ("GEAR_GutDried", 1, 50));
            items.Add(new ("GEAR_Cloth", 1, 100));
        }

        float IExamineActionFailable.GetFailureChance(ExamineActionState state)
        {
            return 10;
        }

        void IExamineActionFailable.OnActionFailure(ExamineActionState state) {}

        bool IExamineActionFailable.ShouldConsumeOnFailure(ExamineActionState state)
        {
            return false;
        }

        float GetNormalizedConditionCap (ExamineActionState state) => state.Subject.m_Repairable.m_RepairConditionCap / 100f / 5f;
    }
}
