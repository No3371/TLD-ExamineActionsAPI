using Il2Cpp;
using TinyJSON2;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class DataDrivenGenericAction : IExamineAction,
                                           IExamineActionProduceItems,
                                           IExamineActionProduceLiquid,
                                           IExamineActionProducePowder,
                                           IExamineActionRequireItems,
                                           IExamineActionRequireLiquid,
                                           IExamineActionRequirePowder,
                                           IExamineActionRequireTool,
                                           IExamineActionCancellable,
                                           IExamineActionInterruptable,
                                           IExamineActionFailable,
                                           IExamineActionHasExternalConstraints,
                                           IExamineActionHasDependendency,
                                           IExamineActionDisplayInfo
    {
        static List<Variant> Decoded { get; set; } = new();
        public DataDrivenGenericAction ()
        {
            Id = "[!ACTION_ID]";
            RequiredInDoorState = Weather.IndoorState.NotSet;
            CanBeCancelled = true;
            ShouldConsumeOnSuccess = true;
            MenuItemLocalizationKey = "[!LOCALIZATION_KEY]";
            ActionButtonLocalizedStringKey = "[!LOCALIZATION_KEY]";
        }
        public static DataDrivenGenericAction? NewWithJson (string json)
        {
            var j = JSON.Load(json);
            if (j == null)
            {
                throw new ArgumentException("Invalid json");
            }
            Decoded.Add(j);
            DataDrivenGenericAction action = j.Make<DataDrivenGenericAction>();
            return action;
        }

        /// <summary>
        /// Melon mods required for this action to be regsitered.
        /// Values in "{Author}.{ModName}" format makes the API looks for any mod with the name and author.
        /// Values in "{ModName} format" makes the API looks for any mod with the name.
        /// </summary>
        [Include]
        public string[]? MelonDependency { get; set; }
        /// <summary>
        /// Gears required for this action to be registered.
        /// For exmaple: { "GEAR_Stone", "GEAR_Stick" } means the API will only register the action when this two types of gear can be loaded at runtime.
        /// </summary>
        [Include]
        public string[]? GearNameDependency { get; set; }
        /// <summary>
        /// C# types required for this action to be registered.
        /// The values are expected to be full type name in format "{Namespace}.{Type}, {Assembly}".
        /// For example: "ExamineActionsAPI.DataDrivenGenericAction.DataDrivenGenericAction, ExamineActionsAPI"
        /// </summary>
        [Include]
        public string[]? CSharpTypeDependency { get; set; }

        /// <summary>
        /// Please refer to IExamineAction
        /// </summary>
        [Include]
        public string Id { get; set; }

        /// <summary>
        /// Please refer to IExamineAction
        /// </summary>
        [Include]
        public string MenuItemLocalizationKey { get; set; }

        /// <summary>
        /// Please refer to IExamineAction
        /// </summary>
        [Include]
        public string? MenuItemSpriteName { get; set; }
        /// <summary>
        /// Please refer to IExamineAction
        /// </summary>
        [Include]
        public string ActionButtonLocalizedStringKey { get; set; }
        [Exclude]
        private LocalizedString? actionButtonLocalizedString;
        [Exclude]
        public LocalizedString ActionButtonLocalizedString
        {
            get => actionButtonLocalizedString ??= new LocalizedString() { m_LocalizationID = ActionButtonLocalizedStringKey };
        }

        [Exclude]
        public IExamineActionPanel? CustomPanel => null;

        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public bool InterruptOnStarving                                           { get; set; }

        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public bool InterruptOnExhausted                                          { get; set; }

        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public bool InterruptOnFreezing                                           { get; set; }

        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public bool InterruptOnDehydrated                                         { get; set; }

        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public bool InterruptOnNonRiskAffliction                                  { get; set; }
        /// <summary>
        /// Please refer to IExamineActionInterruptable
        /// </summary>
        [Include]
        public float NormalizedConditionInterruptThreshold                        { get; set; }
        [Include]
        public IShouldInterruptProvider? ShouldInterruptProvider                  { get; set; }
        /// <summary>
        /// Please refer to IExamineActionCancellable
        /// </summary>
        [Include]
        public bool CanBeCancelled                                                { get; set; }
        /// <summary>
        /// Please refer to IExamineActionCancellable
        /// </summary>
        [Include]
        public bool ShouldConsumeOnCancellation                                   { get; set; }
        /// <summary>
        /// Please refer to IExamineActionFailable
        /// </summary>
        [Include]
        public bool ShouldConsumeOnFailure                                        { get; set; }
        /// <summary>
        /// Please refer to IExamineAction
        /// </summary>
        [Include]
        public bool ShouldConsumeOnSuccess                                        { get; set; }
        /// <summary>
        /// Check for class files named ...SubActionCountProvider for available provider implmentations.
        /// </summary>
        [Include]
        public ISubActionCountProvider? SubActionCountProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...DurationMinuteProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IDurationMinutesProvider? DurationMinuteProvider                   { get; set; }
        /// <summary>
        /// Check for class files named ...FailureChanceProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IFailureChanceProvider? FailureChanceProvider                      { get; set; }
        /// <summary>
        /// Check for class files named ...ProgressSecondProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IProgressSecondProvider? ProgressSecondProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...CanPerformProvider for available provider implmentations.
        /// </summary>
        [Include]
        public ICanPerformProvider? CanPerformProvider                            { get; set; }
        /// <summary>
        /// Check for class files named ...IsActionAvailableProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IIsActionAvailableProvider? IsActionAvailableProvider              { get; set; }
        /// <summary>
        /// Check for class files named ...MaterialItemProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IMaterialItemProvider? MaterialItemProvider                        { get; set; }
        /// <summary>
        /// Check for class files named ...MaterialLiquidProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IMaterialLiquidProvider? MaterialLiquidProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...MaterialPowderProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IMaterialPowderProvider? MaterialPowderProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...ProductItemProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IProductItemProvider? ProductItemProvider                          { get; set; }
        /// <summary>
        /// Check for class files named ...ProductLiquidProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IProductLiquidProvider? ProductLiquidProvider                      { get; set; }
        /// <summary>
        /// Check for class files named ...ProductPowderProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IProductPowderProvider? ProductPowderProvider                      { get; set; }
        /// <summary>
        /// Check for class files named ...ToolOptionsProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IToolOptionsProvider? ToolOptionsProvider                          { get; set; }
        /// <summary>
        /// Check for class files named ...IsValidWeatherProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IIsValidWeatherProvider? IsValidWeatherProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...IsValidTimeProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IIsValidTimeProvider? IsValidTimeProvider                          { get; set; }
        /// <summary>
        /// Check for class files named ...IsPointingToValidObjectProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IIsPointingToValidObjectProvider? IsPointingToValidObjectProvider  { get; set; }
        /// <summary>
        /// Check for class files named ...ConsumingUnitsProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IConsumingUnitsProvider? ConsumingUnitsProvider                    { get; set; }
        /// <summary>
        /// Check for class files named ...ConsumingLiquidLitersProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IConsumingLiquidLitersProvider? ConsumingLiquidLitersProvider      { get; set; }
        /// <summary>
        /// Check for class files named ...ConsumingPowderKgsProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IConsumingPowderKgsProvider? ConsumingPowderKgsProvider            { get; set; }
        /// <summary>
        /// Check for class files named ...AudioNameProvider for available provider implmentations.
        /// </summary>
        [Include]
        public IAudioNameProvider? AudioNameProvider                              { get; set; }
        /// <summary>
        /// Check for class files named ...LightRequirementTypeProvider for available provider implmentations.
        /// </summary>
        [Include]
        public ILightRequirementTypeProvider? LightRequirementTypeProvider        { get; set; }
        [Include]
        public ICallbackProvider[]? OnPerformingCallbackProviders                                    { get; set; }
        [Include]
        public ICallbackProvider[]? OnSuccessCallbackProviders                                    { get; set; }
        [Include]
        public ICallbackProvider[]? OnFailureCallbackProviders                                    { get; set; }
        [Include]
        public ICallbackProvider[]? OnActionInterruptedBySystemCallbackProviders                  { get; set; }
        [Include]
        public ICallbackProvider[]? OnInterruptionCallbackProviders                               { get; set; }
        [Include]
        public ICallbackProvider[]? OnCancellationCallbackProviders                               { get; set; }

        /// <value>NotSet, Outdoors, Indoors</value>
        [Include]
        public Weather.IndoorState RequiredInDoorState { get; set; }

        [Include]
        public IInfoConfigProvider? InfoConfigProvider { get; set; }

        int IExamineAction.GetDurationMinutes(ExamineActionState state)
		=> DurationMinuteProvider?.GetDurationMinutes(state) ?? 10;

        float IExamineActionFailable.GetFailureChance(ExamineActionState state)
        => FailureChanceProvider?.GetFailureChance(state) ?? 0;

        float IExamineAction.GetProgressSeconds(ExamineActionState state)
        => ProgressSecondProvider?.GetProgressSeconds(state) ?? 2.5f;

        bool IExamineAction.CanPerform(ExamineActionState state)
        => CanPerformProvider?.CanPerform(state) ?? true;

        string? IExamineAction.GetAudioName(ExamineActionState state)
        => AudioNameProvider?.GetAudioName(state);

        int IExamineAction.GetSubActionCount(ExamineActionState state)
        => SubActionCountProvider?.GetSubActionCount(state) ?? 1;

        bool IExamineActionCancellable.CanBeCancelled(ExamineActionState state) => CanBeCancelled;

        bool IExamineActionCancellable.ShouldConsumeOnCancellation(ExamineActionState state) => ShouldConsumeOnCancellation;

        bool IExamineActionFailable.ShouldConsumeOnFailure(ExamineActionState state) => ShouldConsumeOnFailure;

        bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => ShouldConsumeOnSuccess;

        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        => ProductLiquidProvider?.GetProductLiquid(state,liquids);

        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        => ProductPowderProvider?.GetProductPowder(state,powders);

        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        => ProductItemProvider?.GetProducts(state, products);

        public void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        => MaterialItemProvider?.GetMaterialItems(state, items);

        public void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        => MaterialPowderProvider?.GetMaterialPowder(state,powders);

        public void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        => MaterialLiquidProvider?.GetMaterialLiquid(state,liquids);

        public bool RequireTool(ExamineActionState state) => ToolOptionsProvider != null;

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        => ToolOptionsProvider?.GetToolOptions(state, tools);

        bool IExamineAction.IsActionAvailable(GearItem item)
        => IsActionAvailableProvider?.IsActionAvailable(item) ?? true;

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state)
        {
            if (OnCancellationCallbackProviders != null)
            foreach (var callback in OnCancellationCallbackProviders)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineActionFailable.OnActionFailure(ExamineActionState state)
        {
            if (OnFailureCallbackProviders != null)
            foreach (var callback in OnFailureCallbackProviders)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state)
        {
            if (OnActionInterruptedBySystemCallbackProviders != null)
            foreach (var callback in OnActionInterruptedBySystemCallbackProviders)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterruption(ExamineActionState state)
        {
            if (OnInterruptionCallbackProviders != null)
            foreach (var callback in OnInterruptionCallbackProviders)
            {
                callback.Run(state);
            }
        }

        bool IExamineActionInterruptable.ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            return ShouldInterruptProvider?.ShouldInterrupt(state, ref message) ?? false;
        }

        bool IExamineActionInterruptable.CanBeInterrupted(ExamineActionState state)
        {
            return InterruptOnDehydrated
                || InterruptOnFreezing
                || InterruptOnExhausted
                || InterruptOnStarving
                || InterruptOnNonRiskAffliction
                || NormalizedConditionInterruptThreshold < 1f
                || ShouldInterruptProvider != null;
        }

        void IExamineAction.OnPerforming(ExamineActionState state)
        {
            if (OnPerformingCallbackProviders != null)
            foreach (var callback in OnPerformingCallbackProviders)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            if (OnSuccessCallbackProviders != null)
            foreach (var callback in OnSuccessCallbackProviders)
            {
                callback.Run(state);
            }
        }
        int IExamineAction.GetConsumingUnits(ExamineActionState state)
        => ConsumingUnitsProvider?.GetConsumingUnits(state) ?? 1;

        float IExamineAction.GetConsumingLiquidLiters(ExamineActionState state)
        => ConsumingLiquidLitersProvider?.GetConsumingLiquidLiters(state) ?? 0f;

        float IExamineAction.GetConsumingPowderKgs(ExamineActionState state)
        => ConsumingPowderKgsProvider?.GetConsumingPowderKgs(state) ?? 0f;
        bool IExamineActionHasExternalConstraints.IsPointingToValidObject(ExamineActionState state, GameObject pointedObject)
        => IsPointingToValidObjectProvider?.IsPointingToValidObject(state, pointedObject) ?? true;

        Weather.IndoorState IExamineActionHasExternalConstraints.GetRequiredIndoorState(ExamineActionState state)
        => RequiredInDoorState;

        bool IExamineActionHasExternalConstraints.IsValidWeather(ExamineActionState state, Weather weather)
        => IsValidWeatherProvider?.IsValidWeather(state, weather) ?? true;

        bool IExamineActionHasExternalConstraints.IsValidTime(ExamineActionState state, TLDDateTimeEAPI time)
        => IsValidTimeProvider?.IsValidTime(state, time) ?? true;

        public ActionsToBlock? GetLightRequirementType(ExamineActionState state)
        => LightRequirementTypeProvider?.GetLightRequirementType(state);

        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            InfoConfigProvider?.GetInfoConfigs(state, configs);
        }
    }

    static class ExampleJsonGen
    {
        public static void LogJsonTemplate ()
        {
            var action = new DataDrivenGenericAction()
            {
                Id = "EXAMPLE_JSON_ACTION",
                MenuItemLocalizationKey = "EXAMPLE_JSON_ACTION",
                ActionButtonLocalizedStringKey = "EXAMPLE_JSON_ACTION",
            };
            action.MelonDependency = new [] { "BA.ExamineActionsAPI" };
            action.GearNameDependency = new [] { "GEAR_Stone" };
            action.CSharpTypeDependency = new [] { "ExamineActionsAPI.DataDrivenGenericAction.DataDrivenGenericAction, ExamineActionsAPI"  };
            action.MenuItemSpriteName = "LOADED_SPRITE_ASSET_NAME";
            action.ProductItemProvider = new SimpleProductItemProvider();
            action.ProductLiquidProvider = new SimpleProductLiquidProvider();
            action.ProductPowderProvider = new SimpleProductPowderProvider();
            action.ProgressSecondProvider = new SimpleProgressSecondProvider() {
                BaseProgressSeconds = 1
            };
            action.CanPerformProvider = new SimpleCanPerformProvider() {
                ValidGearNames = new () {"GEAR_Stone", "GEAR_RawMeatDeer"}
            };
            action.ToolOptionsProvider = new SimpleToolOptionsProvider() {
                CuttingToolTypeFilter = ToolsItem.CuttingToolType.Knife
            };
            action.MaterialItemProvider = new SimpleMaterialItemProvider() {
                Item = new () {
                    new MaterialOrProductSizedBySubActionDef (new ("GEAR_Knife", 1, 100), 0, 1),
                }
            };
            action.MaterialLiquidProvider = new SimpleMaterialLiquidProvider();
            action.MaterialPowderProvider = new SimpleMaterialPowderProvider();
            action.FailureChanceProvider = new SimpleFailureChanceProvider();
            action.DurationMinuteProvider = new SimpleDurationMinutesProvider() {
                BaseDurationMinutes = 5
            };
            action.IsActionAvailableProvider = new SimpleIsActionAvailableProvider() {
                MinNormalizedCondition = 0.1f,
                GunItemFilter = true,
                BowItemFilter = false
            };
            action.IsValidTimeProvider = new SimpleIsValidTimeProvider() {
                MinDay = 3,
                MinTime = 13.1f,
                MaxTime = 15.9f
            };
            action.IsPointingToValidObjectProvider = new SimpleIsPointingToValidObjectProvider();
            action.IsValidWeatherProvider = new SimpleIsValidWeatherProvider() {
                IsFoggy = true
            };
            action.ConsumingUnitsProvider = new SimpleConsumingUnitsProvider();
            action.AudioNameProvider = new SimpleAudioNameProvider();
            action.RequiredInDoorState = Weather.IndoorState.Outdoors;
            MelonLoader.MelonLogger.Msg(TinyJSON2.JSON.Dump(action, TinyJSON2.EncodeOptions.EnforceHierarchyOrder | EncodeOptions.IncludePublicProperties));
        }

        public static void LogMinimalJsonTemplate ()
        {
            var action = new DataDrivenGenericAction()
            {
                Id = "EXAMPLE_JSON_ACTION_MINIMAL",
                MenuItemLocalizationKey = "EXAMPLE_JSON_ACTION_MINIMAL",
                ActionButtonLocalizedStringKey = "EXAMPLE_JSON_ACTION_MINIMAL",
            };
            action.MenuItemSpriteName = "LOADED_SPRITE_ASSET_NAME";
            MelonLoader.MelonLogger.Msg(TinyJSON2.JSON.Dump(action, TinyJSON2.EncodeOptions.EnforceHierarchyOrder | TinyJSON2.EncodeOptions.IncludePublicProperties));
        }

        public static void LogCampingToolExample ()
        {
            var action = new DataDrivenGenericAction()
            {
                Id = "CampingTools_TanningRack_Tan",
                MenuItemLocalizationKey = "Tan",
                ActionButtonLocalizedStringKey = "GAMEPLAY_CT_TRackButton",
            };
            action.MelonDependency = new [] { "Camping Tools" };
            action.IsActionAvailableProvider = new SimpleIsActionAvailableProvider() {
                ValidGearNames = new () { "GEAR_TanningRack"  }
            };
            action.SubActionCountProvider = new SimpleSubActionCountProvider() {
                SubActionCount = 4
            };
            action.MaterialItemProvider = new SubActionIdMappedMaterialItemProvider() {
                ItemBySubActionId = new () {
                    new () { new MaterialOrProductDef ("GEAR_LeatherHide", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_WolfPelt", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_BearHide", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_MooseHide", 1, 100) }
                },
            };
            action.ProductItemProvider = new SubActionIdMappedProductItemProvider() {
                ItemBySubActionId = new () {
                    new () { new MaterialOrProductDef ("GEAR_TanningRackDPS2", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_TanningRackWPS2", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_TanningRackBP2", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_TanningRackMP2", 1, 100) }
                },
            };
            action.DurationMinuteProvider = new SubActionIdMappedDurationMinutesProvider() {
                Map = new () {
                    { 0, 15 },
                    { 1, 15 },
                    { 2, 40 },
                    { 3, 40 },
                }
            };
            action.ProgressSecondProvider = new SimpleProgressSecondProvider() {
                BaseProgressSeconds = 5
            };
            action.AudioNameProvider = new SimpleAudioNameProvider() {
                AudioNameBySubAction = new [] {
                    "Play_CraftingLeatherHide",
                }
            };
            MelonLoader.MelonLogger.Msg(TinyJSON2.JSON.Dump(action, TinyJSON2.EncodeOptions.EnforceHierarchyOrder | TinyJSON2.EncodeOptions.IncludePublicProperties));
        }

        public static void LogHammerCan ()
        {
            var action = new DataDrivenGenericAction()
            {
                Id = "Json_HammerCan",
                MenuItemLocalizationKey = "Hammer",
                ActionButtonLocalizedStringKey = "Hammer",
            };
            action.IsActionAvailableProvider = new SimpleIsActionAvailableProvider() {
                ValidGearNames = new () { "GEAR_RecycledCan" }
            };
            action.DurationMinuteProvider = new SimpleDurationMinutesProvider() {
                BaseDurationMinutes = 10
            };
            action.ProgressSecondProvider = new SimpleProgressSecondProvider() {
                BaseProgressSeconds = 3
            };
            action.ProductItemProvider = new SimpleProductItemProvider() {
                Items = new () {
                    new (new ("GEAR_ScrapMetal", 1, 75), 0, 0)
                }
            };
            action.ToolOptionsProvider = new SimpleToolOptionsProvider() {
                CuttingToolTypeFilter = ToolsItem.CuttingToolType.Hammer
            };
            MelonLoader.MelonLogger.Msg(JSON.Dump(action, TinyJSON2.EncodeOptions.IncludePublicProperties));
        }
    }
}
