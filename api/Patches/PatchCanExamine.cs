using HarmonyLib;
using Il2Cpp;

namespace ExamineActionsAPI
{
    // Enable examine when there are available custom actions
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.CanExamine))]
    internal class PatchCanExamine
    {
        private static void Postfix(ItemDescriptionPage __instance, GearItem gi, ref bool __result)
        {
			if (__result) return;
			if (gi == null) return;
			foreach (var a in ExamineActionsAPI.Instance.RegisteredExamineActions)
			{
				if (!a.IsActionAvailable(gi)) continue;
				__result = true;
				break;
			}
        }
    }
}
