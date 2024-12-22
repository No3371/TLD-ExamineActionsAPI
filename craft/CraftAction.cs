
using Il2Cpp;
using Il2CppTLD.Gear;
using ExamineActionsAPI;
using UnityEngine;

namespace ExamineActionsAPIDemo
{
    // ! TO DO
    // Skill
    // WIP for non-stackable when interrupted/cancelled
    public class CraftAction : IExamineAction,
                               IExamineActionRequireItems,
                               IExamineActionRequireLiquid,
                               IExamineActionRequirePowder,
                               IExamineActionProduceItems,
                               IExamineActionProduceLiquid,
                               IExamineActionProducePowder,
                               IExamineActionRequireTool,
                               IExamineActionHasExternalConstraints,
                               IExamineActionDisplayInfo,
                               IExamineActionCancellable,
                               IExamineActionInterruptable
    {

        public string Id => nameof(ExamineToCraft);

        public string MenuItemLocalizationKey => "Craft";

        public string? MenuItemSpriteName => "ico_craftingWorkbench";
        private LocalizedString actionButtonLocalizedString;

        public LocalizedString ActionButtonLocalizedString { get => actionButtonLocalizedString ??= new LocalizedString() { m_LocalizationID = "Craft"}; }

        public IExamineActionPanel? CustomPanel => null;
        public bool InterruptOnStarving => true;
        public bool InterruptOnExhausted => true;
        public bool InterruptOnFreezing => true;
        public bool InterruptOnDehydrated => true;
        public bool InterruptOnNonRiskAffliction => true;
        public float NormalizedConditionInterruptThreshold => 0.3f;

        public int GetSubActionCount(ExamineActionState state)
		{
			return BlueprintCache.MaterialToBlueprintMap[state.Subject.name].Count;
		}

        public int GetDurationMinutes(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            Panel_Crafting cPanel = InterfaceManager.GetPanel<Panel_Crafting>();
            cPanel.SelectedBPI = b;
            return cPanel.GetFinalCraftingTimeWithAllModifiers();
        }

        public float GetProgressSeconds(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            return UnityEngine.Mathf.Max(1, b.m_DurationMinutes / 20f);
        }

        public bool CanPerform(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];

			if (b.m_RequiredGear.Count + b.m_RequiredLiquid.Count + b.m_RequiredPowder.Count > 5)
				return false;

            if (!BlueprintManager.Instance.IsBlueprintUnlocked(b.name))
            {
                return false;
            }

            if (b.m_UsesPhoto && !b.HasRequiredPhotos())
                return false;

			return true;
        }

        public bool ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return true;
        }

        public int GetConsumingUnits(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];

			foreach (var m in b.m_RequiredGear)
			{
                if (m.m_Item.name == state.Subject.name)
					return m.m_Count;
			}

            throw new Exception("Gear mismatch");
        }

        public bool IsActionAvailable(GearItem item)
        {
			return BlueprintCache.MaterialToBlueprintMap.ContainsKey(item.name);
        }

        public void OnActionDeselected(ExamineActionState state) {}

        public void OnActionInterruptedBySystem(ExamineActionState state) {}

        public void OnActionSelected(ExamineActionState state) {}
        public string? GetAudioName(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            return b?.m_CraftingAudio?.Name;
        }

        public void OnPerforming(ExamineActionState state) {}

        public void OnSuccess(ExamineActionState state)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            Panel_Crafting cPanel = InterfaceManager.GetPanel<Panel_Crafting>();
            cPanel.SelectedBPI = b;
            cPanel.OnCraftingSuccess();
        }

        public void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];

			foreach (var m in b.m_RequiredGear)
			{
				var conf = new MaterialOrProductItemConf(m.m_Item.name, m.m_Count, 100);
				if (m.m_Item.name != state.Subject.name)
					items.Add(conf);
			}
        }

        public void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
			foreach (var m in b.m_RequiredLiquid)
			{
				var conf = new MaterialOrProductLiquidConf(m.m_Liquid, m.m_Volume.ToQuantity(1f), 100);
				if (m.m_Liquid == state.Subject.m_LiquidItem?.LiquidType)
					conf.Liters -= Math.Min(conf.Liters, state.Subject.m_LiquidItem.m_Liquid.ToQuantity(1f));

				if (conf.Liters > 0)
					liquids.Add(conf);
			}
        }

        public void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
			foreach (var m in b.m_RequiredPowder)
			{
				var conf = new MaterialOrProductPowderConf(m.m_Powder, m.m_Quantity.ToQuantity(1f), 100);
				if (m.m_Powder == state.Subject.m_PowderItem?.m_Type)
					conf.Kgs -= Math.Min(conf.Kgs, state.Subject.m_PowderItem.m_Weight.ToQuantity(1f));

				if (conf.Kgs > 0)
					powders.Add(conf);
			}
        }

        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];

			products.Add(new (b.m_CraftedResultGear.name, b.m_CraftedResultCount, 100));
        }

        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
        }

        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
        }

        public bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject)
        {
			var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];

            switch (b.m_RequiredCraftingLocation)
            {
                case CraftingLocation.Forge:
                    if (pointedObject?.GetComponent<Forge>() == null)
                        return false;
                    break;
                case CraftingLocation.Fire:
                    if (pointedObject?.GetComponents<Fire>() == null)
                        return false;
                    else if (b.m_RequiresLitFire && !pointedObject.GetComponent<Fire>().IsBurning())
                        return false;
                    break;
                case CraftingLocation.Workbench:
                    if (pointedObject?.GetComponent<WorkBench>() == null)
                        return false;
                    break;
                case CraftingLocation.AmmoWorkbench:
                    if (pointedObject?.GetComponent<AmmoWorkBench>() == null)
                        return false;
                    break;
                case CraftingLocation.FurnitureWorkbench:
                    if (pointedObject?.GetComponent<FurnitureWorkBench>() == null)
                        return false;
                    break;
            }

            return true;
        }

        public Weather.IndoorState GetRequiredIndoorState(ExamineActionState state) =>  Weather.IndoorState.NotSet;

        public bool IsValidWeather(ExamineActionState state, Weather weather) => true;

        public bool IsValidTime(ExamineActionState state, TLDDateTimeEAPI time) => true;


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            var conf = new InfoItemConfig(new LocalizedString(){ m_LocalizationID = "Location" }, b.m_RequiredCraftingLocation.ToString());
            switch (b.m_RequiredCraftingLocation)
            {
                case CraftingLocation.Forge:
                    conf.Content = Localization.Get("GAMEPLAY_Forge");
                    if (state.GetObjectPointedAt()?.GetComponent<Forge>() == null)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    break;
                case CraftingLocation.Fire:
                    conf.Content = Localization.Get("GAMEPLAY_Fire");
                    if (state.GetObjectPointedAt()?.GetComponents<Fire>() == null)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    else if (b.m_RequiresLitFire && !state.GetObjectPointedAt().GetComponent<Fire>().IsBurning())
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    break;
                case CraftingLocation.Workbench:
                    conf.Content = Localization.Get("GAMEPLAY_WorkBench");
                    if (state.GetObjectPointedAt()?.GetComponent<WorkBench>() == null)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    break;
                case CraftingLocation.AmmoWorkbench:
                    conf.Content = Localization.Get("GAMEPLAY_AmmoWorkBench");
                    if (state.GetObjectPointedAt()?.GetComponent<AmmoWorkBench>() == null)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    break;
                case CraftingLocation.FurnitureWorkbench:
                    conf.Content = Localization.Get("GAMEPLAY_FurnitureWorkBench");
                    if (state.GetObjectPointedAt()?.GetComponent<FurnitureWorkBench>() == null)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                    break;
            }
            configs.Add(conf);
            

			foreach (var m in b.m_RequiredGear)
			{
                if (m.m_Item.name == state.Subject.name)
                {
                    int stack = state.Subject.GetStackableItem()?.m_Units ?? 1;
                    conf = new InfoItemConfig(new LocalizedString(){ m_LocalizationID = "Using" }, $"{m.m_Count} of {stack}");
                    if (stack < m.m_Count)
                        conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
					configs.Add(conf);
                    break;
                }
			}
            
            if (!BlueprintManager.Instance.IsBlueprintUnlocked(b.name))
            {
                conf = new InfoItemConfig(new LocalizedString(){ m_LocalizationID = "Blueprint" }, "Locked");
                conf.LabelColor = conf.ContentColor = ExamineActionsAPI.ExamineActionsAPI.DISABLED_COLOR;
                configs.Add(conf);
            }
        }

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state) {}

        bool IExamineActionCancellable.ShouldConsumeOnCancellation(ExamineActionState state) => false;

        void IExamineActionInterruptable.OnInterruption(ExamineActionState state) {}
        bool IExamineActionRequireTool.RequireTool(ExamineActionsAPI.ExamineActionState state)
        {
            var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            return b.m_RequiredTool != null;
        }

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            var b = BlueprintCache.MaterialToBlueprintMap[state.Subject.name][state.SubActionId];
            var cache = new Il2CppSystem.Collections.Generic.List<GearItem>();
            b.GetToolsAvailableToCraft(GameManager.GetInventoryComponent(),cache);
            foreach (var item in cache)
                tools.Add(item.gameObject);
        }

        public ActionsToBlock? GetLightRequirementType(ExamineActionState state) => ActionsToBlock.Crafting;
    }
}