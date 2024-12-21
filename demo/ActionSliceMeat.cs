using ExamineActionsAPI;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    class ActionSliceMeat : IExamineAction, IExamineActionProduceItems, IExamineActionRequireTool, IExamineActionDisplayInfo
    {
        public ActionSliceMeat() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; }

        string IExamineAction.Id => nameof(ActionSliceMeat);

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

        void IExamineAction.OnPerforming(ExamineActionState state)
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

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => false;

        void IExamineActionProduceItems.GetProducts(ExamineActionsAPI.ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> products)
        {
            products.Add(new (state.Subject.name, 1, 100));
        }

        GearItem IExamineActionProduceItems.OverrideProductPrefab(ExamineActionState state, int index) => state.Subject;

        // We are using the subject as the prefab so we can have an new meat item that every part of it is identical to the subject
        // Then we modify the calories of the new meat (meat weight is calculated from calories)
        void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, GearItem product)
        {
            float ratio = (float) state.Temp[0];
			// MelonLogger.Msg($"==PostProcessProduct#{index} {product.name} {product.m_FoodItem.m_CaloriesRemaining}cal {product.WeightKG}kg {state.Subject.WeightKG}kg ratio={ratio}");
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

        float IExamineActionRequireTool.GetDegradingScale(ExamineActionState state) => 0.2f;


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            var conf = new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Min Weight" },
                "0.1kg"
            );

            if (state.Subject.WeightKG.ToQuantity(1) < 0.1f)
                conf.ContentColor = conf.LabelColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;

            configs.Add(conf);
            configs.Add(new InfoItemConfig(
                new LocalizedString() { m_LocalizationID = "Slice Calories" },
                "25~60"
            ));
        }
        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
    // [HarmonyPatch(typeof(CookingPotItem), nameof(CookingPotItem.SetCookedGearProperties))]
    // internal class PatchSetCookedGearProperties
    // {
    //     private static void Postfix(CookingPotItem __instance, GearItem rawItem, GearItem cookedItem)
    //     {
    //         MelonLogger.Msg($"CookingPotItem.SetCookedGearProperties {rawItem.SerializeToString()} {cookedItem.SerializeToString()}");
    //     }
    // }
    // [HarmonyPatch(typeof(GearItem), nameof(GearItem.Serialize))]
    // internal class PatchSerialize
    // {
    //     private static void Postfix(GearItem __instance, bool useStaticProxy, GearItemSaveDataProxy __result)
    //     {
    //         if (!__instance.name.Contains("MeatDeer")) return;
    //         MelonLogger.Msg($"MeatDeer.Serialize static? {useStaticProxy} {__result.m_InstanceIDProxy} foodItem: {__result.m_FoodItemSerialized}");
    //     }
    // }
    // [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    // internal class PatchDeserialize
    // {
    //     private static void Postfix(GearItem __instance, GearItemSaveDataProxy proxy)
    //     {
    //         if (!__instance.name.Contains("MeatDeer")) return;
    //         MelonLogger.Msg($"MeatDeer.Deserialize {proxy.m_InstanceIDProxy} foodItem: {proxy.m_FoodItemSerialized}");
    //     }
    // }
}
