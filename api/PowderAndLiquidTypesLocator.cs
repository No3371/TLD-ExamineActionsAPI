using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine.AddressableAssets;

namespace ExamineActionsAPI
{

    public static class PowderAndLiquidTypesLocator
    {
        public static PowderType? LoadPowder (string name)
        {
            switch (name)
            {
                case "POWDER_Gunpowder":
                    return GunPowderType;
            }
            if (PowderTypeCache.TryGetValue(name, out PowderType? type))
            {
                return type;
            }
            else
            {
                PowderType? pt = Addressables.LoadAssetAsync<PowderType>(name).Result;
                if (pt == null)
                    MelonLogger.Error($"Failed to retrieve powder type {name} (It's ok this happens on game start)");
                PowderTypeCache.Add(name, pt);
                return pt;
            }
        }

        public static LiquidType? LoadLiquid (string name)
        {
            switch (name)
            {
                case "LIQUID_WaterPottable":
                    return WaterPottableType;
                case "LIQUID_WaterNonPottable":
                    return WaterNonPottableType;
                case "LIQUID_Antiseptic":
                    return AntisepticType;
                case "LIQUID_Kerosene":
                    return KeroseneType;
                case "LIQUID_Accelerant":
                    return AccelerantType;
            }
            if (LiquidTypeCache.TryGetValue(name, out LiquidType? type))
            {
                return type;
            }
            else
            {
                LiquidType? lt = Addressables.LoadAssetAsync<LiquidType>(name).Result;
                if (lt == null)
                    MelonLogger.Error($"Failed to retrieve liquid type {name} (It's ok this happens on game start)");
                LiquidTypeCache.Add(name, lt);
                return lt;
            }
        }
        static Dictionary<string, PowderType> PowderTypeCache { get; set; } = new();
        static Dictionary<string, LiquidType> LiquidTypeCache { get; set; } = new();

        private static PowderType? gunPowderType;

        public static PowderType? GunPowderType
        {
            get
            {
                if (gunPowderType == null)
                {
                    gunPowderType = Addressables.LoadAssetAsync<PowderType>("POWDER_Gunpowder").Result;
                    if (gunPowderType == null) MelonLogger.Error("Failed to retrieve POWDER_GunPowder (It's ok this happens on game start)");
                }
                return gunPowderType;
            }
        }


        private static LiquidType? waterPottableType;
        public static LiquidType? WaterPottableType
        {
            get
            {
                if (waterPottableType == null)
                {
                    waterPottableType = Addressables.LoadAssetAsync<LiquidType>("LIQUID_WaterPotable").Result;
                    if (waterPottableType == null) MelonLogger.Error("Failed to retrieve LIQUID_WaterPotable (It's ok this happens on game start)");
                }
                return waterPottableType;
            }
        }


        private static LiquidType? waterNonPottableType;
        public static LiquidType? WaterNonPottableType
        {
            get
            {
                if (waterNonPottableType == null)
                {
                    waterNonPottableType = Addressables.LoadAssetAsync<LiquidType>("LIQUID_WaterNonPotable").Result;
                    if (waterNonPottableType == null) MelonLogger.Error("Failed to retrieve LIQUID_WaterNonPotable (It's ok this happens on game start)");
                }
                return waterNonPottableType;
            }
        }

        private static LiquidType? keroseneType;
        public static LiquidType? KeroseneType
        {
            get
            {
                if (keroseneType == null)
                {
                    keroseneType = Addressables.LoadAssetAsync<LiquidType>("LIQUID_Kerosene").Result;
                    if (keroseneType == null) MelonLogger.Error("Failed to retrieve LIQUID_Kerosene (It's ok this happens on game start)");
                }
                return keroseneType;
            }
        }

        private static LiquidType? antisepticType;
        public static LiquidType? AntisepticType
        {
            get
            {
                if (antisepticType == null)
                {
                    antisepticType = Addressables.LoadAssetAsync<LiquidType>("LIQUID_Antiseptic").Result;
                    if (antisepticType == null) MelonLogger.Error("Failed to retrieve LIQUID_Antiseptic (It's ok this happens on game start)");
                }
                return antisepticType;
            }
        }

        private static LiquidType? accelerantType;
        /// <summary>
        /// NOTE: HL is not using this as in July 2023
        /// </summary>
        /// <value></value>
        public static LiquidType? AccelerantType
        {
            get
            {
                if (accelerantType == null)
                {
                    accelerantType = Addressables.LoadAssetAsync<LiquidType>("LIQUID_Accelerant").Result;
                    if (accelerantType == null) MelonLogger.Error("Failed to retrieve LIQUID_Accelerant (It's ok this happens on game start)");
                }
                return accelerantType;
            }
        }
    }
}
