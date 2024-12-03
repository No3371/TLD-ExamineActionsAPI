using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    // SelectWindow activate windows by behaviours
    // Need to deactiate those if it's a custom action
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.SelectWindow))]
    internal class PatchSelectWindow
    {
        private static void Postfix(Panel_Inventory_Examine __instance, GameObject window)
        {
            ExamineActionsAPI.VeryVerboseLog($"++++++++POST SelectWindow {window?.name}");
            if (ExamineActionsAPI.Instance.State.Action == null) return;
			if (window == __instance.GetActionToolSelect()) 
            {
                if (__instance.m_GearItem.m_Harvest) __instance.m_HarvestWindow.SetActive(false);
                if (__instance.m_GearItem.m_Repairable) __instance.m_RepairPanel.SetActive(false);
                if (__instance.m_GearItem.m_Cleanable) __instance.m_CleanPanel.SetActive(false);
                if (__instance.m_GearItem.m_Sharpenable) __instance.m_SharpenPanel.SetActive(false);
			    ExamineActionsAPI.VeryVerboseLog($"-POST SelectWindow");
            }
        }
    }
}
