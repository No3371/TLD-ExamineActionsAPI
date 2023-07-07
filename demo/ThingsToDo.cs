
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;

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
			
			// To add actions that depend on other mods
			// We need to check if other mods are loaded
			// (Note: this demo mod has its MelonPriority set to 2000 in AssemblyInfo.cs, this makes it be loaded after other mods)

			// Pure ModComponent mods does not contains code and can only be checked by trying to load their items
			if (GearItem.LoadGearItemPrefab("GEAR_StickPile010") != null)
			{
				this.LoggerInstance.Msg("Found Item Pile mod...");
				ExamineActionsAPI.ExamineActionsAPI.Register(new ActionPiling());
				ExamineActionsAPI.ExamineActionsAPI.Register(new ActionUnpiling());
			}
			if (GearItem.LoadGearItemPrefab("GEAR_4FirCone") != null)
			{
				this.LoggerInstance.Msg("Found Bountiful Foraging mod...");
				ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirConeHarvesting());
				ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirSeedBunch());
			}
		}

	}
}