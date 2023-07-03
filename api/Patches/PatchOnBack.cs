using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    // Official code only back from tool select to main window on back button
    // when it's repairing or harvesting panel
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnBack))]
    internal class PatchOnBack
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            MelonLogger.Msg($"OnBack");
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            // if (state.Action != null
            //  && state.Action is not IExamineActionCancellable
            //  && state.ActionInProgress)
            // {
			// 	MelonLogger.Msg($"Cancelling cancel");
            //     return false;
            // }

			if (state.Action is IExamineActionRequireTool
             && __instance.m_ActionToolSelect.activeInHierarchy)
			{
				__instance.SelectWindow(__instance.m_MainWindow);
				MelonLogger.Msg($"OnBack_Redirect {__instance.m_SelectedButtonIndex}");
				return false;
			}
			return true;
        }
    }
}
