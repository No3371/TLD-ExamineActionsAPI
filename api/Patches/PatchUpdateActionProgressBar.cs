using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    // Official code only back from tool select to main window on back button
    // when it's repairing or harvesting panel
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateActionProgressBar))]
    internal class PatchUpdateActionProgressBar
    {
        private static void Prefix(Panel_Inventory_Examine __instance)
        {
			// MelonLogger.Msg($"PRE UpdateActionProgressBar");
		}
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			// MelonLogger.Msg($"++UpdateActionProgressBar");
            IExamineAction? selected = ExamineActionsAPI.Instance.State.Action;
            ExamineActionState state = ExamineActionsAPI.Instance.State;
            if (state.ActionInProgress)
			{
				// MelonLogger.Msg($"+UpdateActionProgressBar");
				// UpdateActionProgressBar early returns if official actions are not in progress
				// Must replciate the statements ↓
				if (selected is IExamineActionCancellable cancellable && cancellable.CanBeCancelled(state))
				{
					if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
					{
						Cursor.visible = true;
						Cursor.lockState = CursorLockMode.Confined;
					}
					if (InputManager.GetEscapePressed(__instance))
						ExamineActionsAPI.Instance.OnActionCancelled();
				}
				else
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}

				if (selected is IExamineActionInterruptable interruptable && ExamineActionsAPI.Instance.ShouldInterrupt(interruptable))
				{
					state.InterruptionFlag = true;
					__instance.m_GenericProgressBar.GetPanel().Cancel();
				}
				// MelonLogger	.Msg($"-UpdateActionProgressBar");
			}
        }
    }
}
