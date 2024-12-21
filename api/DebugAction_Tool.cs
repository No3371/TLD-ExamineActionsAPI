using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{

    class DebugAction_Tool : IExamineAction, IExamineActionRequireTool, IExamineActionCustomInfo, IExamineActionProducePowder, IExamineActionRequirePowder, IExamineActionRequireLiquid, IExamineActionProduceLiquid
    {
        public DebugAction_Tool()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "TOOL";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "TEST");
			this.Info1.Title.m_LocalizationID = "GGGGe";
        }


        IExamineActionPanel? IExamineAction.CustomPanel => null;
        public string Id => nameof(DebugAction_Products);
        public string MenuItemLocalizationKey => "TOOL";
        public string MenuItemSpriteName => "ico_lightSource_lantern";

        public bool DisableDefaultPanel => false;

        public LocalizedString ActionButtonLocalizedString { get; }

        public InfoItemConfig? Info1 { get; }

        public ActionsToBlock? LightRequirement => null;

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

        public void OnPerforming(ExamineActionState state)
        {
        }

        public void OnActionSelected(ExamineActionState state)
        {
        }

        public void OnActionDeselected(ExamineActionState state)
        {
        }

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
			var inv = GameManager.GetInventoryComponent();
			var list = new Il2CppSystem.Collections.Generic.List<GearItem>();
			inv.GearInInventory("GEAR_Knife", list);
			foreach (var item in list) tools.Add(item.gameObject);
			inv.GearInInventory("GEAR_KnifeJ", list);
			foreach (var item in list) tools.Add(item.gameObject);
			inv.GearInInventory("GEAR_Stone", list);
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

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"TOOL interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 1;
        }

        public bool ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return false;
        }

        void IExamineActionProducePowder.GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        {
            powders.Add(new (PowderAndLiquidTypesLocator.GunPowderType, 0.33f, 100));
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineActionRequirePowder.GetRequiredPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        {
            powders.Add(new (PowderAndLiquidTypesLocator.GunPowderType, 0.05f, 100));
            powders.Add(new (PowderAndLiquidTypesLocator.GunPowderType, 0.11f, 100));
            powders.Add(new (PowderAndLiquidTypesLocator.GunPowderType, 0.05f, 50));
        }

        void IExamineActionRequireLiquid.GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
            liquids.Add(new (PowderAndLiquidTypesLocator.WaterPottableType, 0.05f, 100));
            liquids.Add(new (PowderAndLiquidTypesLocator.WaterPottableType, 0.05f, 50));
        }

        void IExamineActionProduceLiquid.GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
            liquids.Add(new (PowderAndLiquidTypesLocator.WaterPottableType, 0.10f, 100));
            liquids.Add(new (PowderAndLiquidTypesLocator.WaterPottableType, 0.05f, 50));
        }

        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            configs.Add(Info1);
        }
    }
}
