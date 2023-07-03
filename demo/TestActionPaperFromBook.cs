using ExamineActionsAPI;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class TestActionPaperFromBook : IExamineAction, IExamineActionProduceItems, IExamineActionRequireTool
    {
        public TestActionPaperFromBook()
        {
        }

        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; }

        string IExamineAction.Id => nameof(TestActionPaperFromBook);

        string IExamineAction.MenuItemLocalizationKey => "Tear";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Tear" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.m_ResearchItem != null;
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return true;
        }

        void IExamineAction.OnPerform(ExamineActionState state) {}

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return state.SelectedTool == null ? 10 : 3;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return state.SelectedTool == null ? 10 : 3;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;

        (string, int, byte)[] IExamineActionProduceItems.GetProducts(ExamineActionState state)
        {
            return new (string, int, byte)[]{
                ("GEAR_PaperStack", 1, 100),
                ("GEAR_PaperStack", 1, 25),
            };
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
    }
}
