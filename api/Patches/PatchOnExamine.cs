using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    // [HarmonyPatch(typeof(Utils), nameof(Utils.SetActive))]
    // internal class PatchSetActive
    // {
    //     private static void Prefix(Utils __instance, GameObject go, bool active)
    //     {
    //         if (ExamineActionsAPI.Instance.State.Action == null || go.activeInHierarchy == active ) return;
    //         ExamineActionsAPI.VeryVerboseLog($"+PRE SetActive {go.name}: {active}");
    //     }
    //     // private static void Postfix(Utils __instance, GameObject go, bool active)
    //     // {
    //     //     if (ExamineActionsAPI.Instance.State.Action == null || go.activeInHierarchy != active ) return;
    //     //     ExamineActionsAPI.VeryVerboseLog($"+POST SetActive {go?.name}: {active}");
    //     // }
    // }
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.OnExamine))]
    internal class PatchOnExamine
    {
        private static GearItem? lastTriedToExamine;
        /// <summary>
        /// Used to distinguish use/examine.
        /// </summary>
        /// <value></value>
        internal static GearItem? LastTriedToExamine
        {
            get => lastTriedToExamine; set
            {
                lastTriedToExamine = value;
                ExamineActionsAPI.VeryVerboseLog($"LastTriedToExamine: {value?.name}");
            }
        }
        private static void Prefix(Panel_Inventory __instance)
        {
            LastTriedToExamine = __instance.GetCurrentlySelectedGearItem();
            ExamineActionsAPI.VeryVerboseLog($"++++++++OnExamine {LastTriedToExamine?.name}");
            ExamineActionsAPI.Instance.LastTriedToPerformedCache = null;
        }
    }
}
