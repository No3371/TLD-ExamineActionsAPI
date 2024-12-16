using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnProgressBarCancel))]
    internal class PatchOnProgressBarCancel
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (state.ActionInProgress)
			{
                if (ExamineActionsAPI.Instance is IExamineActionCancellable cancellable
                 && cancellable.CanBeCancelled(state))
                    ExamineActionsAPI.Instance.OnActionCancelled();
			}
        }
    }
    // [HarmonyPatch(typeof(Panel_GenericProgressBar), nameof(Panel_GenericProgressBar.Launch), new Type[] { typeof(string), typeof(float), typeof(float), typeof(float), typeof(bool), typeof(OnExitDelegate) })]
    // internal class PatchLaunch
    // {
    //     private static void Postfix(Panel_GenericProgressBar __instance, string name, float seconds, float minutes, float randomFailureThreshold, bool skipRestoreInHands, OnExitDelegate del)
    //     {
	// 		MelonLogger.Msg($"Launch: {name} {seconds}s {minutes}m randomFailureThreshold:{randomFailureThreshold} skipRestoreInHands:{ skipRestoreInHands } { del?.data?.target_type }.{del?.data?.method_name}");
    //     }
    // }
    // [HarmonyPatch(typeof(Panel_GenericProgressBar), nameof(Panel_GenericProgressBar.Launch), new Type[] { typeof(string), typeof(float), typeof(float), typeof(float), typeof(string), typeof(string), typeof(bool), typeof(bool), typeof(OnExitDelegate) })]
    // internal class PatchLaunch2
    // {
    //     private static void Postfix(Panel_GenericProgressBar __instance, string name, float seconds, float minutes, float randomFailureThreshold, string audioName, string voiceName, bool supressHeavyBreathing, bool skipRestoreInHands, OnExitDelegate del)
    //     {
	// 		MelonLogger.Msg($"Launch: {name} {seconds}s {minutes}m randomFailureThreshold:{randomFailureThreshold} skipRestoreInHands:{ skipRestoreInHands } audioName:{ audioName } voiceName:{ voiceName } { del?.data?.target_type }.{del?.data?.method_name}");
    //     }
    // }
    // [HarmonyPatch(typeof(Panel_GenericProgressBar), nameof(Panel_GenericProgressBar.Launch), new Type[] { typeof(string), typeof(float), typeof(float), typeof(float), typeof(Il2CppAK.Wwise.Event), typeof(string), typeof(bool), typeof(bool), typeof(OnExitDelegate) })]
    // internal class PatchLaunch3
    // {
    //     private static void Postfix(Panel_GenericProgressBar __instance, string name, float seconds, float minutes, float randomFailureThreshold, Il2CppAK.Wwise.Event audioEvent, string voiceName, bool supressHeavyBreathing, bool skipRestoreInHands, OnExitDelegate del)
    //     {
	// 		MelonLogger.Msg($"Launch: {name} {seconds}s {minutes}m randomFailureThreshold:{randomFailureThreshold} skipRestoreInHands:{ skipRestoreInHands } audioEvent:{ audioEvent.ID } voiceName:{ voiceName } { del?.data?.target_type }.{del?.data?.method_name}");
    //     }
    // }
}
