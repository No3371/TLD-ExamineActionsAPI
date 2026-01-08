using ExamineActionsAPI;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionPaperFromBooks : IExamineAction, IExamineActionProduceItems, IExamineActionRequireTool
    {
        public ActionPaperFromBooks()
        {
        }

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; }

        string IExamineAction.Id => nameof(ActionPaperFromBooks);

        string IExamineAction.MenuItemLocalizationKey => "Tear";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Tear" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.m_ResearchItem != null || item.name == "GEAR_Book";
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return true;
        }

        void IExamineAction.OnPerforming(ExamineActionState state) {}

        int IExamineAction.GetDurationMinutes(ExamineActionState state)
        {
            return state.SelectedTool == null ? 10 : 3;
        }

        float IExamineAction.GetProgressSeconds(ExamineActionState state)
        {
            return state.SelectedTool == null ? 4 : 2;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => true;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_PaperStack", 1, 100));
            products.Add(new ("GEAR_PaperStack", 1, 25));
            products.Add(new ("GEAR_Newsprint", 1, 25));
        }

        void IExamineActionRequireTool.GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            Inventory inventory = GameManager.GetInventoryComponent();
            tools.Add(null); 
            foreach (var gi in inventory.m_Items)
            if (gi != null)
            {
                ToolsItem toolsItem = ((GearItem) gi).m_ToolsItem;
                if (toolsItem && toolsItem.m_CuttingToolType == ToolsItem.CuttingToolType.Knife)
                {
                    tools.Add(gi);
                }
            }
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
