using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
	[HarmonyPatch(typeof(Panel_Repair), nameof(Panel_Repair.Initialize))]
	internal class PatchInitialize
	{
		internal const int CUSTOM_MENU_ITEM_COUNT = 7;
		private static void Postfix(Panel_Repair __instance)
		{
			var pie = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
			ExamineActionsAPI.VeryVerboseLog($"+Initialize");
			foreach (var it in pie.m_ButtonsMenuItems)
				ExamineActionsAPI.Instance.OfficialActionMenuItems.Add(it);
			for (int i = 0; i < CUSTOM_MENU_ITEM_COUNT; i++)
			{
				var capturedId = i;
				var mi = GameObject.Instantiate(pie.m_MenuItemRepair, pie.m_MenuItemHarvest.transform.parent).GetComponent<Panel_Inventory_Examine_MenuItem>();
				pie.m_ButtonsMenuItems.Add(mi);
				pie.m_Buttons.Add(mi.GetComponent<UIButton>());
				ExamineActionsAPI.Instance.CustomActionMenuItems.Add(mi);

				UILabel subLabel = GameObject.Instantiate<UILabel>(mi.m_LabelTitle, mi.transform);
				subLabel.fontSize -= 4;
				subLabel.transform.localPosition -= new Vector3(0, 12, 0);
				subLabel.color = mi.m_TextColor_Disabled;
				GameObject.Destroy(subLabel.GetComponent<UILocalize>());
				ExamineActionsAPI.Instance.CustomActionMenuItemSubLabels.Add(subLabel);

				ExamineActionsAPI.VeryVerboseLog($"Instantiated menu item: {mi.name}");
				mi.m_ButtonSpriteRef.onClick.Clear();
				mi.gameObject.SetActive(false);
				mi.gameObject.name = $"CustomAction{capturedId}";
				// Action callback = new System.Action(() => OnCustomActionSelected(capturedId));
				EventDelegate.Add(mi.m_ButtonSpriteRef.onClick, new System.Action(() => pie.SelectButton(capturedId + ExamineActionsAPI.Instance.OfficialActionMenuItems.Count))); // Mouse click on the menu item (select)
				pie.m_ButtonDelegates.Add(new System.Action(ExamineActionsAPI.Instance.OnPerformSelectedAction)); // Enter pressed when the menu item selected (perform)
			}

			ExamineActionsAPI.Instance.DefaultPanel = new DefaultPanel(pie, __instance);
			ExamineActionsAPI.VeryVerboseLog($"-Initialize");
		}
	}


}
