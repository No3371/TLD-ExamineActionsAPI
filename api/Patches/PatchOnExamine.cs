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
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.OnEquip))]
    internal class PatchOnEquip
    {
        private static GearItem? lastTriedToUse;
        /// <summary>
        /// Used to distinguish use/examine.
        /// </summary>
        /// <value></value>
        internal static GearItem? LastTriedToUse
        {
            get => lastTriedToUse; set
            {
                lastTriedToUse = value;
                ExamineActionsAPI.VeryVerboseLog($"LastTriedToUse: {value?.name}");
            }
        }
        internal static float lastUse;
        private static void Prefix(Panel_Inventory __instance)
        {
            LastTriedToUse = __instance.GetCurrentlySelectedItem().m_GearItem;
			lastUse = Time.realtimeSinceStartup;
        }
    }
}
