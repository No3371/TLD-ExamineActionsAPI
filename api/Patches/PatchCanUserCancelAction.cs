using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_GenericProgressBar), nameof(Panel_GenericProgressBar.CanUserCancelAction))]
    internal class PatchCanUserCancelAction
    {
        private static void Postfix(Panel_GenericProgressBar __instance, ref bool __result)
        {
            if (!__result) return; // No need to change
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (state.ActionInProgress && state.Action != null)
			{
                if (state.Action is not IExamineActionCancellable
                 || (state.Action is IExamineActionCancellable examineActionCancellable && !examineActionCancellable.CanBeCanceled(state)))
                __result = false;
			}
        }
    }
}
