using HarmonyLib;
using Il2Cpp;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateLabels))]
    internal class PatchUpdateLabels
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            // To prevent official actions being disabled/enabled due to tools list changed
			if (ExamineActionsAPI.Instance.State.Action != null
             && ExamineActionsAPI.Instance.State.SelectingTool) return false;
			return true;
        }
    }
}