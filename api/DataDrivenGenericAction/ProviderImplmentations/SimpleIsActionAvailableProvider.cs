// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleIsActionAvailableProvider : IIsActionAvailableProvider
    {
        public SimpleIsActionAvailableProvider()
        {
            MaxNormalizedCondition = 1;
            MaxStackSize = 999;
        }

        [MelonLoader.TinyJSON.Include]
        public List<string>? ValidGearNames { get; set; }
        [MelonLoader.TinyJSON.Include]
        public float MinNormalizedCondition { get; set; }
        [MelonLoader.TinyJSON.Include]
        public float MaxNormalizedCondition { get; set; }
        [MelonLoader.TinyJSON.Include]
        public int MinStackSize { get; set; }
        [MelonLoader.TinyJSON.Include]
        public int MaxStackSize { get; set; }
        [MelonLoader.TinyJSON.Include]

        public bool? GunItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? BowItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? AmmoItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? BaitItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FoodItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? LureItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? RopeItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ArrowItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FlareItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? SnareItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? StoneItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ToolsItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? TorchItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? EvolveItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ForageItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? LiquidItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? PowderItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? RecipeItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? HeatPadItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? MatchesItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? CharcoalItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ClothingItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FirstAidItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ResearchItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? WildlifeItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? BearSpearItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? BreakDownItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? ForceLockItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? SmashableItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? StackableItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? AmmoCasingItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? CanOpeningItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? CookingPotItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FlashlightItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FuelSourceItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? NoiseMakerItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FireStarterItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? KeroseneLampItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? FlareGunRoundItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? HandheldShortwaveItemFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? NarrativeCollectibleItemFilter { get; set; }

        public bool IsActionAvailable(GearItem item)
        {
            var available = (ValidGearNames?.Contains(item.name) ?? true)
                            && item.GetNormalizedCondition() >= MinNormalizedCondition
                            && item.GetNormalizedCondition() <= MaxNormalizedCondition
                            && (item.GetStackableItem()?.m_Units ?? 1) >= MinStackSize
                            && (item.GetStackableItem()?.m_Units ?? 1) <= MaxStackSize;

            if (!available)
                return false;

            if (GunItemFilter != null)
                available &= (item.m_GunItem != null) == GunItemFilter.Value;

            if (BowItemFilter != null)
                available &= (item.m_BowItem != null) == BowItemFilter.Value;

            if (AmmoItemFilter != null)
                available &= (item.m_AmmoItem != null) == AmmoItemFilter.Value;

            if (BaitItemFilter != null)
                available &= (item.m_BaitItem != null) == BaitItemFilter.Value;

            if (!available)
                return false;

            if (FoodItemFilter != null)
                available &= (item.m_FoodItem != null) == FoodItemFilter.Value;

            if (LureItemFilter != null)
                available &= (item.m_LureItem != null) == LureItemFilter.Value;

            if (RopeItemFilter != null)
                available &= (item.m_RopeItem != null) == RopeItemFilter.Value;

            if (ArrowItemFilter != null)
                available &= (item.m_ArrowItem != null) == ArrowItemFilter.Value;

            if (!available)
                return false;

            if (FlareItemFilter != null)
                available &= (item.m_FlareItem != null) == FlareItemFilter.Value;

            if (SnareItemFilter != null)
                available &= (item.m_SnareItem != null) == SnareItemFilter.Value;

            if (StoneItemFilter != null)
                available &= (item.m_StoneItem != null) == StoneItemFilter.Value;

            if (ToolsItemFilter != null)
                available &= (item.m_ToolsItem != null) == ToolsItemFilter.Value;

            if (!available)
                return false;

            if (TorchItemFilter != null)
                available &= (item.m_TorchItem != null) == TorchItemFilter.Value;

            if (EvolveItemFilter != null)
                available &= (item.m_EvolveItem != null) == EvolveItemFilter.Value;

            if (ForageItemFilter != null)
                available &= (item.m_ForageItem != null) == ForageItemFilter.Value;

            if (LiquidItemFilter != null)
                available &= (item.m_LiquidItem != null) == LiquidItemFilter.Value;

            if (!available)
                return false;

            if (PowderItemFilter != null)
                available &= (item.m_PowderItem != null) == PowderItemFilter.Value;

            if (RecipeItemFilter != null)
                available &= (item.m_RecipeItem != null) == RecipeItemFilter.Value;

            if (HeatPadItemFilter != null)
                available &= (item.m_HeatPadItem != null) == HeatPadItemFilter.Value;

            if (MatchesItemFilter != null)
                available &= (item.m_MatchesItem != null) == MatchesItemFilter.Value;

            if (!available)
                return false;

            if (CharcoalItemFilter != null)
                available &= (item.m_CharcoalItem != null) == CharcoalItemFilter.Value;

            if (ClothingItemFilter != null)
                available &= (item.m_ClothingItem != null) == ClothingItemFilter.Value;

            if (FirstAidItemFilter != null)
                available &= (item.m_FirstAidItem != null) == FirstAidItemFilter.Value;

            if (ResearchItemFilter != null)
                available &= (item.m_ResearchItem != null) == ResearchItemFilter.Value;

            if (!available)
                return false;

            if (WildlifeItemFilter != null)
                available &= (item.m_WildlifeItem != null) == WildlifeItemFilter.Value;

            if (BearSpearItemFilter != null)
                available &= (item.m_BearSpearItem != null) == BearSpearItemFilter.Value;

            if (BreakDownItemFilter != null)
                available &= (item.m_BreakDownItem != null) == BreakDownItemFilter.Value;

            if (ForceLockItemFilter != null)
                available &= (item.m_ForceLockItem != null) == ForceLockItemFilter.Value;

            if (!available)
                return false;

            if (SmashableItemFilter != null)
                available &= (item.m_SmashableItem != null) == SmashableItemFilter.Value;

            if (StackableItemFilter != null)
                available &= (item.m_StackableItem != null) == StackableItemFilter.Value;

            if (AmmoCasingItemFilter != null)
                available &= (item.m_AmmoCasingItem != null) == AmmoCasingItemFilter.Value;

            if (!available)
                return false;

            if (CanOpeningItemFilter != null)
                available &= (item.m_CanOpeningItem != null) == CanOpeningItemFilter.Value;

            if (CookingPotItemFilter != null)
                available &= (item.m_CookingPotItem != null) == CookingPotItemFilter.Value;

            if (FlashlightItemFilter != null)
                available &= (item.m_FlashlightItem != null) == FlashlightItemFilter.Value;

            if (FuelSourceItemFilter != null)
                available &= (item.m_FuelSourceItem != null) == FuelSourceItemFilter.Value;

            if (!available)
                return false;

            if (NoiseMakerItemFilter != null)
                available &= (item.m_NoiseMakerItem != null) == NoiseMakerItemFilter.Value;

            if (FireStarterItemFilter != null)
                available &= (item.m_FireStarterItem != null) == FireStarterItemFilter.Value;

            if (KeroseneLampItemFilter != null)
                available &= (item.m_KeroseneLampItem != null) == KeroseneLampItemFilter.Value;

            if (FlareGunRoundItemFilter != null)
                available &= (item.m_FlareGunRoundItem != null) == FlareGunRoundItemFilter.Value;

            if (!available)
                return false;

            if (HandheldShortwaveItemFilter != null)
                available &= (item.m_HandheldShortwaveItem != null) == HandheldShortwaveItemFilter.Value;

            if (NarrativeCollectibleItemFilter != null)
                available &= (item.m_NarrativeCollectibleItem != null) == NarrativeCollectibleItemFilter.Value;

            return available;
        }
    }

}
