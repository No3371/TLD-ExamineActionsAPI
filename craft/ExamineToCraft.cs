using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;
using System.Collections;
using HarmonyLib;
using ExamineActionsAPI;
using Il2CppTLD.Cooking;

namespace ExamineActionsAPIDemo
{
    internal class ExamineToCraft : MelonMod
    {
		internal static ExamineToCraft Instance { get; private set; }
        public override void OnInitializeMelon()
		{
			Instance = this;
		}

        public override void OnLateInitializeMelon()
        {
			ExamineActionsAPI.ExamineActionsAPI.Register(new CraftAction());
        }

    }

    [HarmonyPatch]
	public static class BlueprintCache
	{
		public static Dictionary<string, List<BlueprintData>> MaterialToBlueprintMap { get; private set; } = new();
		// patched into postfix BlueprintManager.LoadAllUserBlueprints
		// (BlueprintManager.RemoveUserBlueprints is not guaranteed to get called)
		[HarmonyPostfix]
		[HarmonyPriority(Priority.Last)]
		[HarmonyPatch(typeof(Il2CppTLD.Gear.BlueprintManager), nameof(Il2CppTLD.Gear.BlueprintManager.LoadAllUserBlueprints))]
		private static void BlueprintManager_LoadAllUserBlueprints_Postfix(Il2CppTLD.Gear.BlueprintManager __instance)
		{

			int totalMapped = 0;
			foreach (var b in __instance.m_AllBlueprints)
			{
				foreach (var m in b.m_RequiredGear)
				{
					if (!MaterialToBlueprintMap.TryGetValue(m.m_Item.name, out var list))
					{
						list = new();
						MaterialToBlueprintMap.Add(m.m_Item.name, list);
					}
					if (!list.Contains(b))
					{
						list.Add(b);
						totalMapped++;
					}
				}
				foreach (var m in b.m_RequiredLiquid)
				{
					if (!MaterialToBlueprintMap.TryGetValue(m.m_Liquid.name, out var list))
					{
						list = new();
						MaterialToBlueprintMap.Add(m.m_Liquid.name, list);
					}
					if (!list.Contains(b))
					{
						list.Add(b);
						totalMapped++;
					}
				}
				foreach (var m in b.m_RequiredPowder)
				{
					if (!MaterialToBlueprintMap.TryGetValue(m.m_Powder.name, out var list))
					{
						list = new();
						MaterialToBlueprintMap.Add(m.m_Powder.name, list);
					}
					if (!list.Contains(b))
					{
						list.Add(b);
						totalMapped++;
					}
				}
			}

			ExamineToCraft.Instance.LoggerInstance.Msg($"Mapped {MaterialToBlueprintMap.Count} materials to {totalMapped} blueprints.");
		}
	}
}