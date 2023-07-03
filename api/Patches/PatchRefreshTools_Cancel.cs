using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    // [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshTools))]
    // internal class PatchRefreshTools_Cancel
    // {
    //     private static bool Prefix(Panel_Inventory_Examine __instance)
    //     {
    //         MelonLogger.Msg($"+PRE RefreshTools ({__instance.m_Tools.Count} tools / {__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
	// 		if (ExamineActionsAPI.Instance.State.Action is IExamineActionRequireTool
    //          && ExamineActionsAPI.Instance.State.SelectingTool
    //          && !ExamineActionsAPI.Instance.State.Action.DisableDefaultPanel)
	// 		{
	// 			return false;
	// 		}
	// 		return true;
    //     }
    //     private static void Postix(Panel_Inventory_Examine __instance)
    //     {
    //         MelonLogger.Msg($"+POST RefreshTools ({__instance.m_Tools.Count} tools / {__instance.m_ActionToolSelect.gameObject.activeInHierarchy} / { __instance.m_ToolScrollList.m_TargetIndex } / { __instance.m_ToolScrollList.m_SelectedIndex })");
    //     }
    // }
}
