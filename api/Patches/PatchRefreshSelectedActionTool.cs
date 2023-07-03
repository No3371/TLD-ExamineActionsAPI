using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshSelectedActionTool))]
    internal class PatchRefreshSelectedActionTool
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			// MelonLogger.Msg($"PRE RefreshSelectedActionTool ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			// MelonLogger.Msg($"POST RefreshSelectedActionTool ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
            GearItem? gearItem = __instance.GetSelectedTool()?.GetComponent<GearItem>();
            if (gearItem != ExamineActionsAPI.Instance.State.SelectedTool)
                ExamineActionsAPI.Instance.State.SelectedTool = gearItem;
            // if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool
            //  && ExamineActionsAPI.Instance.State.SelectingTool
            //  && !__instance.m_ActionToolSelect.gameObject.activeInHierarchy)
            //  {
            //     __instance.m_ActionToolSelect.gameObject.SetActive(true); // Somehow official code is closing it on tool change
            // }
        }
    }

    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateActionToolSelect))]
    internal class PatchUpdateActionToolSelect
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
            // if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool && ExamineActionsAPI.Instance.State.SelectingTool)
                MelonLogger.Msg($"PRE UpdateActionToolSelect ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            // if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool && ExamineActionsAPI.Instance.State.SelectingTool)
			    MelonLogger.Msg($"POST UpdateActionToolSelect ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
    }
    // [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Update))]
    // internal class PatchUpdate
    // {
    //     private static void Prefix(Panel_Inventory_Examine __instance)
    //     {
    //         if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool && ExamineActionsAPI.Instance.State.SelectingTool)
    //             MelonLogger.Msg($"PRE Update ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
    //     }
    //     private static void Postfix(Panel_Inventory_Examine __instance)
    //     {
    //         if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool && ExamineActionsAPI.Instance.State.SelectingTool)
	// 		    MelonLogger.Msg($"POST Update ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
    //     }
    // }
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.SelectToolByIndex))]
    internal class PatchSelectToolByIndex
    {
        private static void Prefix(Panel_Inventory_Examine __instance, int index)
        {
			MelonLogger.Msg($"PRE SelectToolByIndex {index}  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance, int index)
        {
			MelonLogger.Msg($"POST SelectToolByIndex {index}  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
    }

    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateMainAndToolHybrid))]
    internal class PatchUpdateMainAndToolHybrid
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"PRE UpdateMainAndToolHybrid ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"POST UpdateMainAndToolHybrid  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
    }
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnToolDecrease))]
    internal class PatchOnToolDecrease
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"PRE OnToolDecrease  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"POST OnToolDecrease  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
    }
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnToolIncrease))]
    internal class PatchOnToolIncrease
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"PRE OnToolIncrease  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"POST OnToolIncrease  ({__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
        }
    }
}
