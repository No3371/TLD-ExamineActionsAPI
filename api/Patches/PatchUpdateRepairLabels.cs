using HarmonyLib;
using Il2Cpp;

namespace ExamineActionsAPI
{
    // Prevent exception from official code calculating repair chance
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateRepairLabels))]
    internal class PatchUpdateRepairLabels
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
			if (ExamineActionsAPI.Instance.State.Action != null) return false;
			return true;
        }
    }
}
