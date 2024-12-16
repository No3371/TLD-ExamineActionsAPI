// #define VERY_VERBOSE
using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
    internal class PatchRefreshMainWindow
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			ExamineActionsAPI.Instance.State.Subject = __instance.m_GearItem;
			ExamineActionsAPI.Instance.AvailableCustomActions.Clear();
			foreach (var mi in ExamineActionsAPI.Instance.CustomActionMenuItems) mi.gameObject.SetActive(false);
			if (PatchOnEquip.lastUse > PatchOnExamine.lastExamine)
			{
				return;
			}
	
			ExamineActionsAPI.VeryVerboseLog($"+=======+++PRE RefreshMainWindow");


			foreach (var a in ExamineActionsAPI.Instance.RegisteredExamineActions)
			{
				if (!a.IsActionAvailable(ExamineActionsAPI.Instance.State.Subject)) continue;
				ExamineActionsAPI.Instance.AvailableCustomActions.Add(a);
			}
			
            for (int i = 0; i < ExamineActionsAPI.Instance.AvailableCustomActions.Count; i++)
			{
                IExamineAction? a = ExamineActionsAPI.Instance.AvailableCustomActions[i];
                if (i >= PatchInitialize.CUSTOM_MENU_ITEM_COUNT)
				{
					MelonLogger.Warning($"Too many modded menu items, examine action {a.Id} will not be displayed.");
					continue;
				}
				Panel_Inventory_Examine_MenuItem mi = ExamineActionsAPI.Instance.CustomActionMenuItems[i];
				var ul = mi.GetComponentInChildren<UILocalize>();
				if (ul) ul.key = a.MenuItemLocalizationKey;
				else mi.m_LabelTitle.text = a.MenuItemLocalizationKey;
				mi.m_ButtonSpriteRef.normalSprite = a.MenuItemSpriteName;
				mi.gameObject.SetActive(true);
			}
			ExamineActionsAPI.VeryVerboseLog($"-PRE RefreshMainWindow");
        }
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			ExamineActionsAPI.VeryVerboseLog($"+=======++POST RefreshMainWindow");
			if (PatchOnExamine.LastTriedToExamine == null) return;
			for (int i = 0; i < ExamineActionsAPI.Instance.AvailableCustomActions.Count; i++)
			{
				ExamineActionsAPI.Instance.RefreshCustomActionMenuItemState(i);

                if (ExamineActionsAPI.Instance.LastTriedToPerformedCache == ExamineActionsAPI.Instance.AvailableCustomActions[i])
				{
					ExamineActionsAPI.VeryVerboseLog($"Selecting selected");
                    __instance.SelectButton(i + ExamineActionsAPI.Instance.OfficialActionMenuItems.Count);
				}
			}
			ExamineActionsAPI.VeryVerboseLog($"-POST RefreshMainWindow");
        }
    }
}
