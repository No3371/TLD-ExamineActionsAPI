using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{

    class DebugAction_Tool : IExamineAction, IExamineActionRequireTool, IExamineActionCustomInfo, IExamineActionProducePowder
    {
        public DebugAction_Tool()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "TOOL";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "TEST");
			this.Info1.Title.m_LocalizationID = "Test Title";
        }


        IExamineActionPanel? IExamineAction.CustomPanel => null;
        public string Id => nameof(DebugAction_Products);
        public string MenuItemLocalizationKey => "TOOL";
        public string MenuItemSpriteName => "ico_lightSource_lantern";

        public bool DisableDefaultPanel => false;

        public LocalizedString ActionButtonLocalizedString { get; }

        public InfoItemConfig? Info1 { get; }

        public InfoItemConfig? Info2 => null;

        public ActionsToBlock? LightRequirement => null;

        public bool ConsumeOnFailure => true;

        public bool AllowStarving => false;

        public bool AllowExhausted => false;

        public bool AllowFreezing => false;

        public bool AllowDehydrated => false;

        public bool AllowNonRiskAffliction => false;

        public float MinimumCondition => 0;

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return 30;
        }


        public bool IsActionAvailable(GearItem gi)
        {
			return true;
        }
	
        public bool CanPerform(ExamineActionState state)
        {
			// return state.Subject.GearItemData.name.ToLower().Contains("meat");
			return true;
        }

        public void OnPerform(ExamineActionState state)
        {
			MelonLogger.Msg($"Performing TOOL");
        }

        public void OnActionSelected(ExamineActionState state)
        {
			MelonLogger.Msg($"TOOL selected");
        }

        public void OnActionDeselected(ExamineActionState state)
        {
			MelonLogger.Msg($"TOOL deselected");
        }

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
			var inv = GameManager.GetInventoryComponent();
			var list = new Il2CppSystem.Collections.Generic.List<GearItem>();
			inv.GearInInventory("GEAR_Knife", list);
			foreach (var item in list) tools.Add(item.gameObject);
			inv.GearInInventory("GEAR_KnifeJ", list);
			foreach (var item in list) tools.Add(item.gameObject);
			inv.GearInInventory("GEAR_SewingKit", list);
			foreach (var item in list) tools.Add(item.gameObject);
			inv.GearInInventory("GEAR_Hatchet", list);
			foreach (var item in list) tools.Add(item.gameObject);
        }

        public void DegradeTool(ExamineActionState state, bool result, int elapsedMinutes)
        {
        }

        public void OnSuccess(ExamineActionState state)
        {
        }

        public InfoItemConfig? GetInfo1(ExamineActionState state) => Info1;

        public InfoItemConfig? GetInfo2(ExamineActionState state) => Info2;

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"TOOL interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 1;
        }

        public bool ConsumeOnCancel(ExamineActionState state)
        {
            return false;
        }

        public bool ConsumeOnSuccess(ExamineActionState state)
        {
            return true;
        }

        void IExamineActionProducePowder.GetProductPowder(ExamineActionState state, List<(PowderType, float, byte)> powders)
        {
            powders.Add((PowerTypesLocator.GunPowderType, 0.33f, 100));
        }
    }
}
