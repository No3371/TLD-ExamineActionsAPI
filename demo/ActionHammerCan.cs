using ExamineActionsAPI;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionHammerCan : IExamineAction, IExamineActionProduceItems, IExamineActionRequireTool
    {
        public ActionHammerCan() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; }

        string IExamineAction.Id => nameof(ActionHammerCan);

        string IExamineAction.MenuItemLocalizationKey => "Hammer";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Hammer" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.name == "GEAR_RecycledCan" || (item.m_FoodItem != null && (item.m_FoodItem.m_GearPrefabHarvestAfterFinishEatingNormal?.name == "GEAR_RecycledCan" || item.m_FoodItem.m_GearPrefabHarvestAfterFinishEatingSmashed?.name == "GEAR_RecycledCan"));
        }

        bool IExamineAction.CanPerform(ExamineActionState state) => true;
        void IExamineAction.OnPerform(ExamineActionState state) {}
        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return 10;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 3;
        }
        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => true;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_ScrapMetal", 1, 75));
        }

        // We are using the subject as the prefab so we can have an new meat item that every part of it is identical to the subject
        // Then we modify the calories of the new meat (meat weight is calculated from calories)
        void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, GearItem product) {}

        void IExamineActionRequireTool.GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            Inventory inventory = GameManager.GetInventoryComponent();
            foreach (var gi in inventory.m_Items)
            if (gi != null)
            {
                ToolsItem toolsItem = ((GearItem) gi).m_ToolsItem;
                if (toolsItem && toolsItem.m_CuttingToolType == ToolsItem.CuttingToolType.Hammer)
                {
                    tools.Add(gi);
                }
            }
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
        string? IExamineAction.GetAudioName(ExamineActionsAPI.ExamineActionState state)
        {
            return state.SelectedTool.GearItemData.PutBackAudio.Name;
        }
    }
}
