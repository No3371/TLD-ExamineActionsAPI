using HarmonyLib;
using Il2Cpp;

namespace ExamineActionsAPI
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Update))]
    internal class PatchUpdate_ShouldImmediatelyExitOverlay
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (InterfaceManager.ShouldImmediatelyExitOverlay())
            {
                ExamineActionState state = ExamineActionsAPI.Instance.State;
                if (state.ActionInProgress)
                {
                    __instance.OnProgressBarCancel();
                    state.InterruptionFlag = true;
                    state.InterruptionSystemFlag = true;
                }
                __instance.Enable(false, ComingFromScreenCategory.Inventory_Examine);
                return;
            }
        }
    }
}
