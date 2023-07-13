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
            return Mathf.Max(5, state.Subject.m_Repairable?.m_DurationMinutes/10 ?? 5);
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 3;
        }

        // This action cost nothing but time so to balance it we limit it to sharpen stuff up to 75%
        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.CurrentHP < state.Subject.GearItemData.MaxHP * state.Subject.m_Repairable?.m_RepairConditionCap / 500f;
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
            if (item.m_ClothingItem == null || item.m_Repairable == null) return false;
            if (item.GetNormalizedCondition() >= item.m_Repairable.m_RepairConditionCap / 5) return false;
            return true;
        }

        void IExamineActionCancellable.OnActionCanceled(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterrupted(ExamineActionState state) {}

        void IExamineAction.OnPerform(ExamineActionState state) {}

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            state.Subject.SetNormalizedHP(state.Subject.m_Repairable.m_RepairConditionCap/10f);
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo1(ExamineActionState state)
        {
            return new InfoItemConfig(new LocalizedString() { m_LocalizationID = "Repair Result" } , $"{state.Subject.m_Repairable.m_RepairConditionCap/5f:0.0}%");
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo2(ExamineActionState state) 
        {
            return null;
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
            return 50;
        }

        void IExamineActionFailable.OnActionFailed(ExamineActionState state) {}

        bool IExamineActionFailable.ConsumeOnFailure(ExamineActionState state)
        {
            return false;
        }
    }
}
