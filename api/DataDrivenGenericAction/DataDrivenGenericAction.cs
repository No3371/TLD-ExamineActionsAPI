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
            IsCancellable = true;
            ConsumeOnSuccess = true;
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
            action.MaterialItemProvider = new SimpleSingleMaterialItemProvider() {
                Item = new () {
                    new MaterialOrProductSizedBySubActionDef (new ("GEAR_Knife", 1, 100), 0, 1),
                }
            };
            action.MaterialLiquidProvider = new SimpleMaterialLiquidProvider();
            action.MaterialPowderProvider = new SimpleSingleMaterialPowderProvider();
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
            action.GetConsumingUnitsProvider = new SimpleGetConsumingUnitsProvider();
            action.GetAudioNameProvider = new SimpleGetAudioNameProvider();
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
            action.IsActionAvailableProvider = new SimpleIsActionAvailableProvider() {
                ValidGearNames = new () { "GEAR_TanningRack"  }
            };
            action.SubActionCountProvider = new SimpleSubActionCountProvider() {
                SubActionCount = 4
            };
            action.MaterialItemProvider = new SimpleMultiMaterialItemProvider() {
                ItemBySubActionId = new () {
                    new () { new MaterialOrProductDef ("GEAR_LeatherHide", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_WolfPelt", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_BearHide", 1, 100) },
                    new () { new MaterialOrProductDef ("GEAR_MooseHide", 1, 100) }
                },
            };
            action.ProductItemProvider = new SimpleMultiProductItemProvider() {
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
            action.GetAudioNameProvider = new SimpleGetAudioNameProvider() {
                AudioNameBySubAction = new [] {
                    "Play_CraftingLeatherHide",
                }
            };
            MelonLoader.MelonLogger.Msg(MelonLoader.TinyJSON.JSON.Dump(action, MelonLoader.TinyJSON.EncodeOptions.EnforceHierarchyOrder | MelonLoader.TinyJSON.EncodeOptions.IncludePublicProperties));
        }

        [Include]
        public (string Author, string Name)[]? MelonDependency { get; set; }
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
        public bool IsCancellable                                                 { get; set; }
        [Include]
        public bool ConsumeOnCancellation                                         { get; set; }
        [Include]
        public bool ConsumeOnFailure                                              { get; set; }
        [Include]
        public bool ConsumeOnSuccess                                              { get; set; }
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
        public IGetConsumingUnitsProvider? GetConsumingUnitsProvider              { get; set; }
        [Include]
        public IGetConsumingLiquidLitersProvider? GetConsumingLiquidLitersProvider{ get; set; }
        [Include]
        public IGetConsumingPowderKgsProvider? GetConsumingPowderKgsProvider      { get; set; }
        [Include]
        public IGetAudioNameProvider? GetAudioNameProvider                        { get; set; }
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

        int IExamineAction.CalculateDurationMinutes(ExamineActionState state)
		=> DurationMinuteProvider?.CalculateDurationMinutes(state) ?? 10;

        float IExamineActionFailable.CalculateFailureChance(ExamineActionState state)
        => FailureChanceProvider?.CalculateFailureChance(state) ?? 0;

        float IExamineAction.CalculateProgressSeconds(ExamineActionState state)
        => ProgressSecondProvider?.CalculateProgressSeconds(state) ?? 2.5f;

        bool IExamineAction.CanPerform(ExamineActionState state)
        => CanPerformProvider?.CanPerform(state) ?? true;

        string? IExamineAction.GetAudioName(ExamineActionState state)
        => GetAudioNameProvider?.GetAudioName(state);

        int IExamineAction.GetSubActionCount(ExamineActionState state)
        => SubActionCountProvider?.GetSubActionCount(state) ?? 1;

        bool IExamineActionCancellable.CanBeCancelled(ExamineActionState state) => IsCancellable;

        bool IExamineActionCancellable.ConsumeOnCancellation(ExamineActionState state) => ConsumeOnCancellation;

        bool IExamineActionFailable.ConsumeOnFailure(ExamineActionState state) => ConsumeOnFailure;

        bool IExamineAction.ConsumeOnSuccess(ExamineActionState state) => ConsumeOnSuccess;

        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        => ProductLiquidProvider?.GetProductLiquid(state,liquids);

        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        => ProductPowderProvider?.GetProductPowder(state,powders);

        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        => ProductItemProvider?.GetProducts(state, products);

        public void GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        => MaterialItemProvider?.GetRequiredItems(state, items);

        public void GetRequiredPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powders)
        => MaterialPowderProvider?.GetRequiredPowder(state,powders);

        public void GetRequiredLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
        => MaterialLiquidProvider?.GetRequiredLiquid(state,liquids);

        public bool RequireTool(ExamineActionState state) => IsToolRequired;

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        => ToolOptionsProvider?.GetToolOptions(state, tools);

        bool IExamineAction.IsActionAvailable(GearItem item)
        => IsActionAvailableProvider?.IsActionAvailable(item) ?? true;

        void IExamineActionCancellable.OnActionCancelled(ExamineActionState state)
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

        void IExamineAction.OnPerform(ExamineActionState state)
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
        => GetConsumingUnitsProvider?.GetConsumingUnits(state) ?? 1;

        float IExamineAction.GetConsumingLiquidLiters(ExamineActionState state)
        => GetConsumingLiquidLitersProvider?.GetConsumingLiquidLiters(state) ?? 0f;

        float IExamineAction.GetConsumingPowderKgs(ExamineActionState state)
        => GetConsumingPowderKgsProvider?.GetConsumingPowderKgs(state) ?? 0f;
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
