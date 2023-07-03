using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    // Inlined
    [HarmonyPatch(typeof(Panel_GenericProgressBar), nameof(Panel_GenericProgressBar.CanUserCancelAction))]
    internal class PatchCanUserCancelAction
    {
        /// <summary>
        /// Prevent the cancel button from sh
        private static void Postfix(Panel_GenericProgressBar __instance, ref bool __result)
        {
            if (!__result) return; // No need to change
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (state.ActionInProgress && state.Action != null && state.Action is not IExamineActionCancellable)
			{
                __result = false;
			}
        }
    }
}
