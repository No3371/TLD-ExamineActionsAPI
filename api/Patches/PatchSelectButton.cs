using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.SelectButton))]
    internal class PatchSelectButton
    {
        private static void Postfix(Panel_Inventory_Examine __instance, int index)
        {
			ExamineActionsAPI.Instance.DeselectActiveCustomAction();

			ExamineActionsAPI.VeryVerboseLog($"POST SelectButton {__instance.m_SelectedButtonIndex} ({index}?)");
			// var customActIndex = index - Panel_Inventory_Examine_Enable.activeState.ActiveOfficialActions;
			if (index >= ExamineActionsAPI.Instance.OfficialActionMenuItems.Count)
			{
				__instance.m_HarvestWindow.SetActive(false);
				__instance.m_RepairPanel.SetActive(false);
				__instance.m_CleanPanel.SetActive(false);
				__instance.m_SharpenPanel.SetActive(false);
				__instance.m_RifleUnloadPanel.SetActive(false);
				__instance.m_ReadPanel.SetActive(false);
				__instance.m_RefuelPanel.SetActive(false);
				__instance.m_SafehouseCustomizationRepairPanel.SetActive(false);
				
				ExamineActionsAPI.Instance.OnCustomActionSelected(index - ExamineActionsAPI.Instance.OfficialActionMenuItems.Count);

				// __instance.GetActionToolSelect().gameObject.SetActive(false);sw
				// __instance.m_Tool_ConfirmButtonLabel.parent.gameObject.SetActive(false);

			}
			else
			{
				// __instance.GetActionToolSelect().gameObject.SetActive(true);
				// __instance.m_Tool_ConfirmButtonLabel.parent.gameObject.SetActive(true);
			}
        }
    }
    // [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.CheckSelectedButton))]
    // internal class PatchCheckSelectedButton
    // {
    //     private static bool Prefix(Panel_Inventory_Examine __instance)
	// 	{
	// 		if (PatchUseInventoryItem.LastUsedAndNotSelectedAction != null)
	// 		{
	// 			ExamineActionsAPI.VeryVerboseLog($"Cancelling CheckSelectedButton ({ __instance.m_SelectedButtonIndex }) ");
	// 			return false;
	// 		}
	// 		return true;
	// 	}
	// }
}
