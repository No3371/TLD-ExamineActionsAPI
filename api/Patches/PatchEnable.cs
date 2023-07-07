#define VERY_VERBOSE
using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
	// NOTE: Enable (false) is not getting called now

    // [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Enable), new Type[] { typeof(bool), typeof(ComingFromScreenCategory) })]
    // internal class PatchEnable1
    // {
	// 	static bool init = false;
		
    //     private static bool Prefix(Panel_Inventory_Examine __instance, bool enable)
	// 	{
	// 		ExamineActionsAPI.VeryVerboseLog($"---------- PRE Enable: {enable}");
	// 		if (!enable)
	// 		{
	// 			PatchOnExamine.LastTriedToExamine = null;
	// 			PatchOnEquip.LastTriedToUse = null;
	// 		}
	// 		return true;
	// 	}

    //     private static void Postfix(Panel_Inventory_Examine __instance, bool enable)
    //     {
	// 		ExamineActionsAPI.VeryVerboseLog($"---------- POST Enable: {enable}");
	// 		if (!enable)
	// 		{
	// 			PatchOnExamine.LastTriedToExamine = null;
	// 			PatchOnEquip.LastTriedToUse = null;
	// 		}
	// 		// if (agent == null)
	// 		// {
	// 		// 	agent = __instance.gameObject.AddComponent<ExamineActionAPIAgent>();
	// 		// }
	// 		// if (!init)
	// 		// {
	// 		// 	for (int i = 0; i < MENU_ITEM_COUNT; i++)
	// 		// 	{
	// 		// 		var capturedId = i;
	// 		// 		var _mi = GameObject.Instantiate(__instance.m_MenuItemRepair, __instance.m_MenuItemHarvest.transform.parent).GetComponent<Panel_Inventory_Examine_MenuItem>();
	// 		// 		__instance.m_ButtonsMenuItems.Add(_mi);
	// 		// 		__instance.m_Buttons.Add(_mi.GetComponent<UIButton>());
	// 		// 		menuItems.Add(_mi);
	// 		// 		ExamineActionsAPI.VeryVerboseLog($"Instantiated menu item: {_mi.name}");
	// 		// 		_mi.m_ButtonSpriteRef.onClick.Clear();
	// 		// 		_mi.gameObject.SetActive(false);
	// 		// 		_mi.gameObject.name = $"CustomAction{capturedId}";
    //         //         // Action callback = new System.Action(() => OnCustomActionSelected(capturedId));
    //         //         EventDelegate.Add(_mi.m_ButtonSpriteRef.onClick, new System.Action(() => __instance.SelectButton(capturedId + OFFICIAL_MENU_ITEM_COUNT))); // Mouse click on the menu item (select)
	// 		// 		__instance.m_ButtonDelegates.Add(new System.Action(Panel_Inventory_Examine_Enable.OnPerformSelectedAction)); // Enter pressed when the menu item selected (perform)
	// 		// 	}

	// 		// 	panel = new GeneralPanel(__instance);
	// 		// 	init = true;
	// 		// }

	// 		// if (__instance.m_GearItem.m_ResearchItem != null) return; // Reading interface is prioritized.
	// 		// ExamineActionsAPI.Instance.State.ActiveOfficialActions = 0;

	// 		// float offset = 0;
	// 		// int autoSelect = -1;
	// 		// for (int i = 0 ; i < ExamineActionsAPI.Instance.CustomActionMenuItems.Count; i++)
	// 		// {
    //         //     Panel_Inventory_Examine_MenuItem it = ExamineActionsAPI.Instance.CustomActionMenuItems[i];
	// 		// 	if (!it.isActiveAndEnabled) continue;
    //         //     float y = it.transform.localPosition.y;
	// 		// 	if (y <= offset) offset = y + __instance.m_ButtonSpacing;
	// 		// 	autoSelect = i;
	// 		// }
	// 		// // ExamineActionsAPI.VeryVerboseLog($"MenuItem offset: {offset}");

	// 		// int configured = 0;
	// 		// foreach (var a in activeActions)
	// 		// {
	// 		// 	if (configured >= PatchInitialize.MENU_ITEM_COUNT)
	// 		// 	{
	// 		// 		MelonLogger.Warning($"Too many modded menu items, examine action {a.Id} will not be displayed.");
	// 		// 		continue;
	// 		// 	}
	// 		// 	Panel_Inventory_Examine_MenuItem mi = ExamineActionsAPI.Instance.CustomActionMenuItems[configured];
	// 		// 	mi.SetDisabled(a.CanPerformAction(ExamineActionsAPI.Instance.State));
	// 		// 	mi.gameObject.SetActive(false);
	// 		// 	var ul = mi.GetComponentInChildren<UILocalize>();
	// 		// 	if (ul) ul.key = a.MenuItemLocalizationKey;
	// 		// 	else mi.m_LabelTitle.text = a.MenuItemLocalizationKey;
	// 		// 	mi.m_ButtonSpriteRef.normalSprite = a.MenuItemSpriteName;
	// 		// 	mi.transform.localPosition = new Vector3(0, offset);
	// 		// 	offset += __instance.m_ButtonSpacing;
	// 		// 	mi.gameObject.SetActive(true);
	// 		// 	configured++;
	// 		// }

	// 		// if (autoSelect != -1) __instance.SelectButton(autoSelect);

    //     }

    // }
}
