using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI
{
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
