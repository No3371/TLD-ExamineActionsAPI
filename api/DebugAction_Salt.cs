using Il2Cpp;
using MelonLoader;

namespace ExamineActionsAPI
{
    class DebugAction_Salt : IExamineAction, IExamineActionRequireItems, IExamineActionProduceItems, IExamineActionFailable, IExamineActionDisplayInfo, IExamineActionCancellable
    {
        public DebugAction_Salt()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Salt";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "SALT");
			this.Info1.Title.m_LocalizationID = "Test Title2";
        }


        IExamineActionPanel? IExamineAction.CustomPanel => null;
        public string Id => nameof(DebugAction_Salt);
        public string MenuItemLocalizationKey => "GAMEPLAY_BFM_Drain";
        public string MenuItemSpriteName => "ico_lightSource_lantern";

        public bool DisableDefaultPanel => false;

        public LocalizedString ActionButtonLocalizedString { get; }

        public InfoItemConfig? Info1 { get; }

        public ActionsToBlock? LightRequirement => ActionsToBlock.Repair;

        public bool ConsumeMaterialsOnFailure => false;

        public bool AllowStarving => false;

        public bool AllowExhausted => false;

        public bool AllowFreezing => false;

        public bool AllowDehydrated => false;

        public bool AllowNonRiskAffliction => false;

        public float MinimumCondition => 0.5f;

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return 2;
        }


        public bool IsActionAvailable(GearItem gi)
        {
            return gi.name.ToLower().Contains("meat");
        }
	
        public bool CanPerform(ExamineActionState state)
        {
			return true;
        }

        void IExamineActionRequireItems.GetMaterialItems(ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> items)
        {
            items.Add(new ("GEAR_Stone", 2, 8));
            items.Add(new ("GEAR_Stone", 1, 55));
            items.Add(new ("GEAR_Broth", 1, 55));
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_Knife", 1, 100));
            products.Add(new ("GEAR_WhiskyFlask", 1, 8));
            products.Add(new ("GEAR_BeerBottle", 1, 55));
            products.Add(new ("GEAR_Broth", 2, 55));
        }


        public float GetFailureChance(ExamineActionState state)
        {
            return 95f;
        }

        public void OnActionFailure(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnSuccess(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnPerforming(ExamineActionState state)
        {
			MelonLogger.Msg($"Performing Salt");
        }

        public void OnActionSelected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt selected");
        }

        public void OnActionDeselected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt deselected");
        }


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            configs.Add(Info1);
        }

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 6f;
        }

        public bool ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool KeepMaterialOnInterruption(ExamineActionState state, string matGearName)
        {
            return false;
        }

        bool IExamineActionFailable.ShouldConsumeOnFailure(ExamineActionState state)
        {
            return false;
        }

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state)
        {
            
        }

        bool IExamineActionCancellable.ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return true;
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
    class DebugAction_Salt2 : IExamineAction, IExamineActionRequireItems, IExamineActionProduceItems, IExamineActionFailable, IExamineActionDisplayInfo, IExamineActionCancellable
    {
        public DebugAction_Salt2()
        {
			ActionButtonLocalizedString = new LocalizedString();
			ActionButtonLocalizedString.m_LocalizationID = "Salt";
			this.Info1 = new InfoItemConfig(new LocalizedString(), "SALT");
			this.Info1.Title.m_LocalizationID = "Test Title2";
			this.Info2 = new InfoItemConfig(new LocalizedString(), "SALT");
			this.Info2.Title.m_LocalizationID = "Test Title2";
        }


        IExamineActionPanel? IExamineAction.CustomPanel => null;
        public string Id => nameof(DebugAction_Salt);
        public string MenuItemLocalizationKey => "GAMEPLAY_BFM_Drain";
        public string MenuItemSpriteName => "ico_lightSource_lantern";

        public bool DisableDefaultPanel => false;

        public LocalizedString ActionButtonLocalizedString { get; }

        public InfoItemConfig? Info1 { get; }
        public InfoItemConfig? Info2 { get; }

        public ActionsToBlock? LightRequirement => ActionsToBlock.Repair;

        public bool ConsumeMaterialsOnFailure => false;

        public bool AllowStarving => false;

        public bool AllowExhausted => false;

        public bool AllowFreezing => false;

        public bool AllowDehydrated => false;

        public bool AllowNonRiskAffliction => false;

        public float MinimumCondition => 0.5f;

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return 2;
        }


        public bool IsActionAvailable(GearItem gi)
        {
            return gi.name.ToLower().Contains("meat");
        }
	
        public bool CanPerform(ExamineActionState state)
        {
			return true;
        }

        void IExamineActionRequireItems.GetMaterialItems(ExamineActionState state, System.Collections.Generic.List<MaterialOrProductItemConf> items)
        {
            items.Add(new ("GEAR_Stone", 2, 8));
            items.Add(new ("GEAR_Stone", 1, 55));
            items.Add(new ("GEAR_Broth", 1, 55));
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new ("GEAR_Knife", 1, 100));
            products.Add(new ("GEAR_WhiskyFlask", 1, 8));
            products.Add(new ("GEAR_BeerBottle", 1, 55));
            products.Add(new ("GEAR_Broth", 2, 55));
        }


        public float GetFailureChance(ExamineActionState state)
        {
            return 95f;
        }

        public void OnActionFailure(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnSuccess(ExamineActionState state)
        {
            // throw new NotImplementedException();
        }

        public void OnPerforming(ExamineActionState state)
        {
			MelonLogger.Msg($"Performing Salt");
        }

        public void OnActionSelected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt selected");
        }

        public void OnActionDeselected(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt deselected");
        }


        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            configs.Add(Info1);
            configs.Add(Info2);
        }

        public void OnInterrupted(ExamineActionState state)
        {
			MelonLogger.Msg($"Salt interrupted");
        }

        public float CalculateProgressSeconds(ExamineActionState state)
        {
            return 6f;
        }

        public bool ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool KeepMaterialOnInterruption(ExamineActionState state, string matGearName)
        {
            return false;
        }

        bool IExamineActionFailable.ShouldConsumeOnFailure(ExamineActionState state)
        {
            return false;
        }

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state)
        {
            
        }

        bool IExamineActionCancellable.ConsumeOnCancellation(ExamineActionState state)
        {
            return false;
        }

        public bool ShouldConsumeOnSuccess(ExamineActionState state)
        {
            return true;
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
    }
}
