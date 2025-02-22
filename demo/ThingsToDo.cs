﻿
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
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionPiling());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionUnpiling());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirConeHarvesting());
			ExamineActionsAPI.ExamineActionsAPI.Register(new ActionFirSeedBunch());

		}
	}
}