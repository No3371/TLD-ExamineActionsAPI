using ExamineActionsAPI;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionAcornPreparing : IExamineAction, IExamineActionProduceItems
    {
        public ActionAcornPreparing() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        string IExamineAction.Id => nameof(ActionAcornPreparing);

        string IExamineAction.MenuItemLocalizationKey => "Prepare";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Prepare" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name == "GEAR_Acorn";
            
        }

        bool IExamineAction.CanPerform(ExamineActionState state) => true;
        void IExamineAction.OnPerforming(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            // For this action, we are using the sub action feature as "how many acorn to prepare"
            // So the SubAction#0 would be "prepare 1 acorn"
            // SubActionId is 0-based so we add 1 to it to properly calculate the time
            return (state.SubActionId + 1) * 10;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            // Make it so preparing more than 1 acorn takes 2 seconds to finish
            return state.SubActionId == 0? 1 : 2;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => true;
        // For this action, we are using the sub action feature as "how many acorn to prepare"
        // So the SubAction#0 would be "prepare 1 acorn"
        // which will consume 1 acorn
        // SubActionId is 0-based so we add 1 to it
        int IExamineAction.GetConsumingUnits(ExamineActionState state) => state.SubActionId + 1;
        
        // For this action, we are using the sub action feature as "how many acorn to prepare"
        // So it's important to tell the API how many SubActions are availabe based on how many acorns in the stack the player is examining
        // And we are limiting it to up to 4 because 4 is the number that acorns can be prepared into 1 large portion prepared acrons
        int IExamineAction.GetSubActionCount(ExamineActionState state) => Mathf.Min(4, state.Subject.m_StackableItem?.m_Units ?? 1);

        // For this action, we are using the sub action feature as "how many acorn to prepare"
        // So according to the SubActionId we are returning different units or make it a large portion one
        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(state.SubActionId switch
            {
                0 => new ("GEAR_AcornShelled", 1, 100),
                1 => new ("GEAR_AcornShelled", 2, 100),
                2 => new ("GEAR_AcornShelled", 3, 100),
                3 => new ("GEAR_AcornShelledBig", 1, 100)
            });
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
