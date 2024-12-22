using Il2Cpp;
using MelonLoader.TinyJSON;
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
                                           IExamineActionHasDependendency
    {
        public DataDrivenGenericAction ()
        {
            Id = "!!!ACTION_ID_UNSET!!!";
            RequiredInDoorState = Weather.IndoorState.NotSet;
            IsToolRequired = true;
            CanBeCancelled = true;
            ShouldConsumeOnSuccess = true;
        }
        public static DataDrivenGenericAction? NewWithJson (string json)
        {
            var j = MelonLoader.TinyJSON.Decoder.Decode(json) as ProxyObject;
            if (j == null)
            {
                throw new ArgumentException("Invalid json");
            }
            DataDrivenGenericAction action = j.Make<DataDrivenGenericAction>();
            return action;
        }
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
            action.ProductItemProvider = new SimpleSingleProductItemProvider();
            action.ProductLiquidProvider = new SimpleSingleProductLiquidProvider();
            action.ProductPowderProvider = new SimpleSingleProductPowderProvider();
            action.ProgressSecondProvider = new SimpleProgressSecondProvider() {
                BaseProgressSeconds = 1
            };
            action.CanPerformProvider = new SimpleCanPerformProvider() {
                ValidGearNames = new () {"GEAR_Stone", "GEAR_RawMeatDeer"}
            };
            action.ToolOptionsProvider = new SimpleToolOptionsProvider() {
                CuttingToolTypeFilter = ToolsItem.CuttingToolType.Knife
            };
            action.IsToolRequired = true;
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
            MelonLoader.MelonLogger.Msg(MelonLoader.TinyJSON.JSON.Dump(action, MelonLoader.TinyJSON.EncodeOptions.EnforceHierarchyOrder | EncodeOptions.IncludePublicProperties));
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
            MelonLoader.MelonLogger.Msg(MelonLoader.TinyJSON.JSON.Dump(action, MelonLoader.TinyJSON.EncodeOptions.EnforceHierarchyOrder | MelonLoader.TinyJSON.EncodeOptions.IncludePublicProperties));
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
            MelonLoader.MelonLogger.Msg(MelonLoader.TinyJSON.JSON.Dump(action, MelonLoader.TinyJSON.EncodeOptions.EnforceHierarchyOrder | MelonLoader.TinyJSON.EncodeOptions.IncludePublicProperties));
        }

        [Include]
        public string[]? MelonDependency { get; set; }
        [Include]
        public string[]? GearNameDependency { get; set; }
        [Include]
        public string[]? CSharpTypeDependency { get; set; }

        [Include]
        public string Id { get; set; }

        [Include]
        public string MenuItemLocalizationKey { get; set; }

        [Include]
        public string? MenuItemSpriteName { get; set; }
        [Include]
        public string ActionButtonLocalizedStringKey { get; set; }
        [Exclude]
        private LocalizedString actionButtonLocalizedString;
        [Exclude]
        public LocalizedString ActionButtonLocalizedString
        {
            get => actionButtonLocalizedString ??= new LocalizedString() { m_LocalizationID = ActionButtonLocalizedStringKey };
        }

        [Exclude]
        public IExamineActionPanel? CustomPanel => null;

        [Include]
        public bool InterruptOnStarving                                           { get; set; }

        [Include]
        public bool InterruptOnExhausted                                          { get; set; }

        [Include]
        public bool InterruptOnFreezing                                           { get; set; }

        [Include]
        public bool InterruptOnDehydrated                                         { get; set; }

        [Include]
        public bool InterruptOnNonRiskAffliction                                  { get; set; }
        [Include]
        public float NormalizedConditionInterruptThreshold                        { get; set; }
        [Include]
        public bool CanBeCancelled                                                 { get; set; }
        [Include]
        public bool ShouldConsumeOnCancellation                                         { get; set; }
        [Include]
        public bool ShouldConsumeOnFailure                                              { get; set; }
        [Include]
        public bool ShouldConsumeOnSuccess                                              { get; set; }
        [Include]
        public bool IsToolRequired                                                { get; set; }
        [Include]
        public ISubActionCountProvider? SubActionCountProvider                    { get; set; }
        [Include]
        public IDurationMinutesProvider? DurationMinuteProvider                   { get; set; }
        [Include]
        public IFailureChanceProvider? FailureChanceProvider                      { get; set; }
        [Include]
        public IProgressSecondProvider? ProgressSecondProvider                    { get; set; }
        [Include]
        public ICanPerformProvider? CanPerformProvider                            { get; set; }
        [Include]
        public IIsActionAvailableProvider? IsActionAvailableProvider              { get; set; }
        [Include]
        public IMaterialItemProvider? MaterialItemProvider                        { get; set; }
        [Include]
        public IMaterialLiquidProvider? MaterialLiquidProvider                    { get; set; }
        [Include]
        public IMaterialPowderProvider? MaterialPowderProvider                    { get; set; }
        [Include]
        public IProductItemProvider? ProductItemProvider                          { get; set; }
        [Include]
        public IProductLiquidProvider? ProductLiquidProvider                      { get; set; }
        [Include]
        public IProductPowderProvider? ProductPowderProvider                      { get; set; }
        [Include]
        public IToolOptionsProvider? ToolOptionsProvider                          { get; set; }
        [Include]
        public IIsValidWeatherProvider? IsValidWeatherProvider                    { get; set; }
        [Include]
        public IIsValidTimeProvider? IsValidTimeProvider                          { get; set; }
        [Include]
        public IIsPointingToValidObjectProvider? IsPointingToValidObjectProvider  { get; set; }
        [Include]
        public IConsumingUnitsProvider? ConsumingUnitsProvider              { get; set; }
        [Include]
        public IConsumingLiquidLitersProvider? ConsumingLiquidLitersProvider{ get; set; }
        [Include]
        public IConsumingPowderKgsProvider? ConsumingPowderKgsProvider      { get; set; }
        [Include]
        public IAudioNameProvider? AudioNameProvider                        { get; set; }
        [Include]
        public ILightRequirementTypeProvider? LightRequirementTypeProvider        { get; set; }
        [Include]
        public ICallback[]? OnPerformCallbacks       { get; set; }
        [Include]
        public ICallback[]? OnSuccessCallbacks       { get; set; }
        [Include]
        public ICallback[]? OnFailureCallbacks       { get; set; }
        [Include]   
        public ICallback[]? OnInterruptionCallbacks  { get; set; }
        [Include]
        public ICallback[]? OnCancellationCallbacks  { get; set; }

        /// <value>NotSet, Outdoors, Indoors</value>
        [Include]
        Weather.IndoorState RequiredInDoorState { get; set; }

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

        public bool RequireTool(ExamineActionState state) => IsToolRequired;

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        => ToolOptionsProvider?.GetToolOptions(state, tools);

        bool IExamineAction.IsActionAvailable(GearItem item)
        => IsActionAvailableProvider?.IsActionAvailable(item) ?? true;

        void IExamineActionCancellable.OnActionCancellation(ExamineActionState state)
        {
            if (OnCancellationCallbacks != null)
            foreach (var callback in OnCancellationCallbacks)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnActionDeselected(ExamineActionState state) {}

        void IExamineActionFailable.OnActionFailure(ExamineActionState state)
        {
            if (OnFailureCallbacks != null)
            foreach (var callback in OnFailureCallbacks)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state) {}

        void IExamineAction.OnActionSelected(ExamineActionState state) {}

        void IExamineActionInterruptable.OnInterruption(ExamineActionState state)
        {
            if (OnInterruptionCallbacks != null)
            foreach (var callback in OnInterruptionCallbacks)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnPerforming(ExamineActionState state)
        {
            if (OnPerformCallbacks != null)
            foreach (var callback in OnPerformCallbacks)
            {
                callback.Run(state);
            }
        }

        void IExamineAction.OnSuccess(ExamineActionState state)
        {
            if (OnSuccessCallbacks != null)
            foreach (var callback in OnSuccessCallbacks)
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
    }
}
