using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    // [HarmonyPatch(typeof(Panel_HUD), nameof(Panel_HUD.ItemProgressBarCheck))]
    // internal class PatchItemProgressBarCheck
    // {
    //     /// <summary>
    //     /// Prevent the cancel button from sh
    //     private static void Postfix(Panel_HUD __instance)
    //     {
    //         ExamineActionState state = ExamineActionsAPI.Instance.State;
    //         if (state.ActionInProgress && state.Action is not IExamineActionCancellable
    //          && __instance.m_ClickHoldCancelButton.transform.parent.parent.gameObject.activeSelf == true)
	// 		{
	// 		    MelonLogger.Msg($"{__instance.m_ClickHoldCancelButton.transform.parent.parent.gameObject.name}: OFF");
    //             __instance.m_ClickHoldCancelButton.transform.parent.parent.gameObject.SetActive(false);
	// 		}
    //     }
    // }

    // Prevent actions cancelled by clicking the cancel button
    [HarmonyPatch(typeof(Panel_HUD), nameof(Panel_HUD.DoClickHoldCancel))]
    internal class PatchDoClickHoldCancel
    {
        private static bool Prefix(Panel_HUD __instance)
        {
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (!state.ActionInProgress || state.Action == null)
            {
                return true;
            }
            return (state.Action is IExamineActionCancellable examineActionCancellable && examineActionCancellable.CanBeCanceled(state));
        }
    }
    // Prevent actions cancelled by clicking the cancel button
    [HarmonyPatch(typeof(AccelTimePopup), nameof(AccelTimePopup.OnCancel))]
    internal class PatchOnCancel
    {
        private static bool Prefix(AccelTimePopup __instance)
        {
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (!state.ActionInProgress || state.Action == null)
            {
                return true;
            }
            return (state.Action is IExamineActionCancellable examineActionCancellable && examineActionCancellable.CanBeCanceled(state));
        }
    }
}