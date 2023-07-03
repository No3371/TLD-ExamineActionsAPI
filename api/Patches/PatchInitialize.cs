using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Initialize))]
    internal class PatchInitialize
    {
		internal const int CUSTOM_MENU_ITEM_COUNT = 7;
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			MelonLogger.Msg($"+Initialize");
			foreach (var it in __instance.m_ButtonsMenuItems)
				ExamineActionsAPI.Instance.OfficialActionMenuItems.Add(it);
			for (int i = 0; i < CUSTOM_MENU_ITEM_COUNT; i++)
			{
				var capturedId = i;
				var mi = GameObject.Instantiate(__instance.m_MenuItemRepair, __instance.m_MenuItemHarvest.transform.parent).GetComponent<Panel_Inventory_Examine_MenuItem>();
				__instance.m_ButtonsMenuItems.Add(mi);
				__instance.m_Buttons.Add(mi.GetComponent<UIButton>());
				ExamineActionsAPI.Instance.CustomActionMenuItems.Add(mi);

                UILabel subLabel = GameObject.Instantiate<UILabel>(mi.m_LabelTitle, mi.transform);
				subLabel.fontSize -= 4;
				subLabel.transform.localPosition -= new Vector3(0, 12, 0);
				subLabel.color = mi.m_TextColor_Disabled;
				GameObject.Destroy(subLabel.GetComponent<UILocalize>());
                ExamineActionsAPI.Instance.CustomActionMenuItemSubLabels.Add(subLabel);

				MelonLogger.Msg($"Instantiated menu item: {mi.name}");
				mi.m_ButtonSpriteRef.onClick.Clear();
				mi.gameObject.SetActive(false);
				mi.gameObject.name = $"CustomAction{capturedId}";
				// Action callback = new System.Action(() => OnCustomActionSelected(capturedId));
				EventDelegate.Add(mi.m_ButtonSpriteRef.onClick, new System.Action(() => __instance.SelectButton(capturedId + ExamineActionsAPI.Instance.OfficialActionMenuItems.Count))); // Mouse click on the menu item (select)
				__instance.m_ButtonDelegates.Add(new System.Action(ExamineActionsAPI.Instance.OnPerformSelectedAction)); // Enter pressed when the menu item selected (perform)
			}

			ExamineActionsAPI.Instance.DefaultPanel = new DefaultPanel(__instance);
			MelonLogger.Msg($"-Initialize");
        }
    }
}
