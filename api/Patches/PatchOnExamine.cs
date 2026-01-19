using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
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
        internal static float lastExamine;
        private static void Prefix(Panel_Inventory __instance)
        {
            LastTriedToExamine = __instance.GetCurrentlySelectedItem().m_GearItem;
            lastExamine = Time.realtimeSinceStartup;
            ExamineActionsAPI.Instance.LastTriedToPerformedCache = null;
        }
    }
}
