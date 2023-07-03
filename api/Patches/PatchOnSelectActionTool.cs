using HarmonyLib;
using Il2Cpp;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnSelectActionTool))]
    internal class PatchOnSelectActionTool
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
			if (ExamineActionsAPI.Instance.State.Action != null) return false;
			return true;
        }
    }
}