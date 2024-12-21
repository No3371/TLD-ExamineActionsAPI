using Il2Cpp;
using Il2CppTLD.Cooking;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{

    class DebugAction_Cancel : IExamineAction, IExamineActionCancellable/*, IExamineActionProduceLiquid*/
    {
        public string Id => nameof(DebugAction_Cancel);

        public string MenuItemLocalizationKey => "Cancellable";

        public string MenuItemSpriteName => null;

        public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Cancellable" };

        public IExamineActionPanel? CustomPanel => null;
        int IExamineAction.GetSubActionCount(ExamineActionState state) => 2;
        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return state.SubActionId == 0 ? 30 : 10;
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return state.SubActionId == 0 ? 30 : 10;
        }

        public bool CanPerform(ExamineActionState state)
        {
            return true;
        }

        public bool IsActionAvailable(GearItem item)
        {
            return true;
        }

        public void OnActionDeselected(ExamineActionState state) {}

        public void OnActionSelected(ExamineActionState state) {}

        public void OnPerforming(ExamineActionState state) {}

        public void OnSuccess(ExamineActionState state)
        {
            // foreach (var v in PowerTypesLocator.PowderTypes.Values)
            //     MelonLogger.Warning(v.name);
            
        }

        public bool ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return false;
        }

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state) {}
        bool IExamineActionCancellable.ShouldConsumeOnCancellation(ExamineActionState state) => false;

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        // void IExamineActionProduceLiquid.GetProductLiquid(ExamineActionState state, List<(GearLiquidTypeEnum, float, byte)> liquids)
        // {
        //     liquids.Add((GearLiquidTypeEnum.Water, 1, 100));
        //     liquids.Add((GearLiquidTypeEnum.Water, 11, 50));
        // }
    }
}
