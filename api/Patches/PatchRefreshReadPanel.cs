using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI
{
    // Prevent read panel from showing up automatically when examining instead of using
    // (HL runs it in update)
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshReadPanel))]
    internal class PatchRefreshReadPanel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
			return PatchOnEquip.lastUse > PatchOnExamine.lastExamine;
                // || PatchOnExamine.LastTriedToExamine == null
        }
    }
}
