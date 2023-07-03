using ExamineActionsAPI;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class TestActionSliceMeat : IExamineAction, IExamineActionProduceItems, IExamineActionRequireTool, IExamineActionCustomInfo
    {
        public TestActionSliceMeat() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; }

        string IExamineAction.Id => nameof(TestActionSliceMeat);

        string IExamineAction.MenuItemLocalizationKey => "Slice";

        string IExamineAction.MenuItemSpriteName => null;

        LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Slice" };

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item.m_FoodItem != null && (item.m_FoodItem.m_IsMeat || item.m_FoodItem.m_IsFish);
        }

        bool IExamineAction.CanPerform(ExamineActionState state)
        {
            return state.Subject.m_FoodItem.m_CaloriesRemaining > 100;
        }

        void IExamineAction.OnPerform(ExamineActionState state)
        {
            float ratio = UnityEngine.Random.Range(25,60) / state.Subject.m_FoodItem.m_CaloriesRemaining;
            state.Temp.Add(0, ratio);
        }

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
        {
            return 1;
        }

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        {
            return 1;
        }
        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            float ratio = 1 - (float) state.Temp[0];
            state.Subject.WeightKG *= ratio;
            state.Subject.m_FoodItem.m_CaloriesRemaining *= ratio;
        }

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => false;

        (string, int, byte)[] IExamineActionProduceItems.GetProducts(ExamineActionState state)
        {
            return new (string, int, byte)[]{
                (state.Subject.name, 1, 100),
            };
        }

        GearItem IExamineActionProduceItems.OverrideProductPrefabs(ExamineActionState state, int index) => state.Subject;

        void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, GearItem product)
        {
            float ratio = (float) state.Temp[0];
			// MelonLogger.Msg($"==PostProcessProduct#{index} {product.name} {product.m_FoodItem.m_CaloriesRemaining}cal {product.WeightKG}kg {state.Subject.WeightKG}kg ratio={ratio}");
            float weight = ratio * state.Subject.WeightKG;
            product.CurrentHP = state.Subject.CurrentHP;
            product.m_FoodItem.m_CaloriesRemaining = ratio * state.Subject.m_FoodItem.m_CaloriesRemaining;
			// MelonLogger.Msg($"--PostProcessProduct#{index} {product.name} {product.m_FoodItem.m_CaloriesRemaining}cal {product.WeightKG}kg {state.Subject.WeightKG}kg ratio={ratio}");
        }

        void IExamineActionRequireTool.GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            Inventory inventory = GameManager.GetInventoryComponent();
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

        float IExamineActionRequireTool.CalculateDegradingScale(ExamineActionState state) => 0.2f;

        InfoItemConfig? IExamineActionCustomInfo.GetInfo1(ExamineActionState state)
        {
            return new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Min Weight" },
                "0.1kg"
            );
        }

        InfoItemConfig? IExamineActionCustomInfo.GetInfo2(ExamineActionState state)
        {
            return new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Slice Calories" },
                "25~60"
            );
        }
    }
}
