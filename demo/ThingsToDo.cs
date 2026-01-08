
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;
using System.Collections;

namespace ExamineActionsAPIDemo
{
    internal class ThingsToDo : MelonMod
    {
		internal static ThingsToDo Instance { get; private set; }
        public override void OnInitializeMelon()
		{
			Instance = this;
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionPaperFromBooks());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionBruteForceSharpening());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionSliceMeat());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionAcornPreparing());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionDisposingRuined());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionUnloadingFuel());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFieldRepair());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionHammerCan());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionMakeTorch());
			var itemPileCompat = true;
			itemPileCompat |= ExamineActionsAPI.ExamineActionsAPI.Register(new ActionPiling());
			itemPileCompat |= ExamineActionsAPI.ExamineActionsAPI.Register(new ActionUnpiling());
			if (!itemPileCompat)
			{
				LoggerInstance.Warning("Some or all ItemPile mod compatibility is not enabled.");
			}
			var bountifulForagingCompat = true;
			bountifulForagingCompat |= ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirConeHarvesting());
			bountifulForagingCompat |= ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirSeedBunch());
			if (!bountifulForagingCompat)
			{
				LoggerInstance.Warning("Some or all Bountiful Foraging mod compatibility is not enabled.");
			}
		}
	}
}