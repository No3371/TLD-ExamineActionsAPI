using ExamineActionsAPI;
using Il2Cpp;
using UnityEngine;
using System.Collections.Generic;
using ExamineActionsAPI.DataDrivenGenericAction;

namespace ExamineActionsAPIDemo
{
    /// <summary>
    /// A slow process to convert fats from CandleLight into 100% torches.
    /// </summary>
    class ActionMakeTorch : IExamineAction,
                            IExamineActionRequireItems,
                            IExamineActionProduceItems,
                            IExamineActionRequireTool,
                            IExamineActionHasExternalConstraints,
                            IExamineActionInterruptable,
                            IExamineActionCancellable,
                            IExamineActionHasDependendency,
                            IExamineActionDisplayInfo
    {
        public string[]? MelonDependency { get; set; }
        public string[]? GearNameDependency { get; set; } = { "GEAR_AnimalFat" };
        public string[]? CSharpTypeDependency { get; set; }

        public ActionMakeTorch() {}
        IExamineActionPanel? IExamineAction.CustomPanel => null;

        public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Make Torch" };

        string IExamineAction.Id => nameof(ActionMakeTorch);

        string IExamineAction.MenuItemLocalizationKey => "Make Torch";

        string IExamineAction.MenuItemSpriteName => "ico_Crafting";

        bool IExamineAction.IsActionAvailable(GearItem item)
        {
            return item != null && item.name == "GEAR_AnimalFat";
        }

        bool IExamineAction.CanPerform(ExamineActionState state) {
            if (state.Subject.m_FoodItem.m_CaloriesRemaining < 900)
            {
                state.CustomWarningMessageOnBlocked = "Requires full block of fat";
                return false;
            }
            if (state.Subject.GetNormalizedCondition() < 0.5f)
            {
                state.CustomWarningMessageOnBlocked = "Requires 50%+ condition";
                return false;
            }
            return true;
        }

        void IExamineAction.OnPerforming(ExamineActionState state) {}

        int IExamineAction.GetDurationMinutes(ExamineActionState state)
        {
            return 10;
        }

        float IExamineAction.GetProgressSeconds(ExamineActionState state)
        {
            return 10f;
        }

        void IExamineAction.OnSuccess(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => true;

        void IExamineActionRequireItems.GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
            items.Add(new MaterialOrProductItemConf("GEAR_Cloth", 1, 100));
        }

        void IExamineActionProduceItems.GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            products.Add(new MaterialOrProductItemConf("GEAR_Torch", 1, 100));
        }

        GearItem IExamineActionProduceItems.OverrideProductPrefab(ExamineActionState state, int index) => null!;

        void IExamineActionProduceItems.PostProcessProduct(ExamineActionState state, int index, GearItem product)
        {
            product.CurrentHP = product.GearItemData.MaxHP;
        }

        void IExamineActionProduceItems.PostProcessProductPreview(ExamineActionState state, int index, GearItem product)
        {
            product.CurrentHP = product.GearItemData.MaxHP;
        }

        void IExamineActionRequireTool.GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            Inventory inventory = GameManager.GetInventoryComponent();
            foreach (var itemObj in inventory.m_Items)
            {
                if (itemObj != null)
                {
                    GearItem? gi = itemObj.m_GearItem;
                    if (gi == null) continue;
                    switch (gi.name)
                    {
                        case "GEAR_RecycledCan":
                        case "GEAR_CookingPot":
                            tools.Add(gi.gameObject);
                            break;
                    }
                }
            }
        }

        float IExamineActionRequireTool.GetDegradingScale(ExamineActionState state) => 0f;

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}
        
        string? IExamineAction.GetAudioName(ExamineActionState state) => "Play_CraftingGeneric";

        // IExamineActionHasExternalConstraints Implementation
        bool IExamineActionHasExternalConstraints.IsPointingToValidObject(ExamineActionState state, GameObject pointedObject)
        {
            if (pointedObject == null) return false;
            
            Fire fire = pointedObject.GetComponent<Fire>();
            if (fire == null) fire = pointedObject.GetComponentInChildren<Fire>();
            if (fire == null) fire = pointedObject.GetComponentInParent<Fire>();

            bool valid = fire != null && fire.IsBurning();
            if (!valid)
            {
                state.CustomWarningMessageOnBlocked = "Requires a burning fire";
            }
            return valid;
        }

        Weather.IndoorState IExamineActionHasExternalConstraints.GetRequiredIndoorState(ExamineActionState state) => Weather.IndoorState.NotSet;

        bool IExamineActionHasExternalConstraints.IsValidWeather(ExamineActionState state, Weather weather) => true;

        bool IExamineActionHasExternalConstraints.IsValidTime(ExamineActionState state, TLDDateTimeEAPI time) => true;

        // IExamineActionInterruptable Implementation
        void IExamineActionInterruptable.OnInterruption(ExamineActionState state) {}

        bool IExamineActionInterruptable.ShouldConsumeOnInterruption(ExamineActionState state) => false;

        ActionsToBlock? IExamineActionInterruptable.GetLightRequirementType(ExamineActionState state) => null;

        bool IExamineActionInterruptable.InterruptOnStarving => true;

        bool IExamineActionInterruptable.InterruptOnExhausted => true;

        bool IExamineActionInterruptable.InterruptOnFreezing => true;

        bool IExamineActionInterruptable.InterruptOnDehydrated => true;

        bool IExamineActionInterruptable.InterruptOnNonRiskAffliction => false;

        float IExamineActionInterruptable.NormalizedConditionInterruptThreshold => 0f;

        bool IExamineActionInterruptable.ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            // Check if fire is still valid and burning
            GameObject obj = GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(2.5f);
            if (obj == null) return true;
            
            Fire fire = obj.GetComponent<Fire>();
            if (fire == null) fire = obj.GetComponentInChildren<Fire>();
            if (fire == null) fire = obj.GetComponentInParent<Fire>();

            bool interrupt = fire == null || !fire.IsBurning();
            if (interrupt)
            {
                message = Localization.Get("Fire went out...");
            }
            return interrupt;
        }

        // IExamineActionCancellable Implementation
        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state) {}

        bool IExamineActionCancellable.ShouldConsumeOnCancellation(ExamineActionState state) => false;

        InfoItemConfig info1 => new InfoItemConfig(
            new LocalizedString() { m_LocalizationID = "Required" },
            "900 cal"
        );
        InfoItemConfig info2 => new InfoItemConfig(
            new LocalizedString() { m_LocalizationID = "Required" },
            "50+% fresh"
        );

        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            configs.Add(info1);
            configs.Add(info2);
        }

        // static ActionMakeTorch()
        // {
        //     var action = new DataDrivenGenericAction()
        //     {
        //         GearNameDependency = new string[] { "GEAR_AnimalFat" },
        //         Id = "Json_MakeTorch",
        //         MenuItemLocalizationKey = "Make Torch",
        //         ActionButtonLocalizedStringKey = "Make Torch",
        //         MenuItemSpriteName = "ico_Crafting",
        //         ShouldConsumeOnSuccess = true,
        //         CanBeCancelled = true,
        //         InterruptOnStarving = true,
        //         InterruptOnExhausted = true,
        //         InterruptOnFreezing = true,
        //         InterruptOnDehydrated = true,
        //         InterruptOnNonRiskAffliction = false,
        //         NormalizedConditionInterruptThreshold = 0f,
        //         RequiredInDoorState = Weather.IndoorState.NotSet
        //     };

        //     action.IsActionAvailableProvider = new SimpleIsActionAvailableProvider()
        //     {
        //         ValidGearNames = new List<string>() { "GEAR_AnimalFat" }
        //     };

        //     action.DurationMinuteProvider = new SimpleDurationMinutesProvider()
        //     {
        //         BaseDurationMinutes = 10
        //     };

        //     action.ProgressSecondProvider = new SimpleProgressSecondProvider()
        //     {
        //         BaseProgressSeconds = 10f
        //     };

        //     action.MaterialItemProvider = new SimpleMaterialItemProvider()
        //     {
        //         Item = new List<MaterialOrProductSizedBySubActionDef>()
        //         {
        //             new MaterialOrProductSizedBySubActionDef(new MaterialOrProductDef("GEAR_Cloth", 1, 100), 0, 0)
        //         }
        //     };

        //     action.ProductItemProvider = new SimpleProductItemProvider()
        //     {
        //         Items = new List<MaterialOrProductSizedBySubActionDef>()
        //         {
        //             new MaterialOrProductSizedBySubActionDef(new MaterialOrProductDef("GEAR_Torch", 1, 100), 0, 0)
        //         }
        //     };

        //     action.AudioNameProvider = new SimpleAudioNameProvider()
        //     {
        //         AudioNameBySubAction = new string[] { "Play_CraftingGeneric" }
        //     };

        //     action.ToolOptionsProvider = new SpecificGearToolOptionsProvider()
        //     {
        //         ValidGearNames = new List<string>() { "GEAR_RecycledCan", "GEAR_CookingPot" }
        //     };

        //     action.IsPointingToValidObjectProvider = new IsPointingToBurningFireProvider();
        //     action.ShouldInterruptProvider = new FireExtinguishingShouldInterruptProvider();

        //     action.CanPerformProvider = new LogicAndCanPerformProvider()
        //     {
        //         Providers = new ICanPerformProvider[]
        //         {
        //             new MinCaloriesCanPerformProvider()
        //             {
        //                 MinCalories = 900,
        //                 BlockedMessage = "Requires full block of fat"
        //             },
        //             new SimpleCanPerformProvider()
        //             {
        //                 MinGearNormalizedCondition = 0.5f,
        //                 MaxGearNormalizedCondition = 1.0f,
        //                 MinStackSize = 0,
        //                 MaxStackSize = 999
        //             }
        //         }
        //     };

        //     action.InfoConfigProvider = new SimpleInfoConfigProvider()
        //     {
        //         InfoItems = new List<SimpleInfoConfigProvider.InfoItem>()
        //         {
        //             new SimpleInfoConfigProvider.InfoItem() { Title = "Required", Content = "900 cal" },
        //             new SimpleInfoConfigProvider.InfoItem() { Title = "Required", Content = "50+% fresh" },
        //         }
        //     };

        //     var json = TinyJSON2.JSON.Dump(action, TinyJSON2.EncodeOptions.EnforceHierarchyOrder | TinyJSON2.EncodeOptions.IncludePublicProperties);
        //     MelonLoader.MelonLogger.Msg(json);

        //     ExamineActionsAPI.ExamineActionsAPI.TryRegisterWithJson(json);
        // }
    }
}
