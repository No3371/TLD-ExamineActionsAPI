using Il2CppSystem.IO;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace ExamineActionsAPI
{
    public static class PowerAndLiquidTypesLocator
    {
        private static PowderType gunPowderType;

        public static PowderType GunPowderType
        {
            get
            {
                if (gunPowderType == null)
                {
                    gunPowderType = Addressables.LoadAsset<PowderType>("POWDER_GunPowder").Result;
                }
                return gunPowderType;
            }
        }


        private static LiquidType waterPottableType;
        public static LiquidType WaterPottableType
        {
            get
            {
                if (waterPottableType == null)
                {
                    waterPottableType = Addressables.LoadAsset<LiquidType>("LIQUID_WaterPotable").Result;
                }
                return waterPottableType;
            }
        }
        // public static IReadOnlyDictionary<string, PowderType> PowderTypes
        // {
        //     get
        //     {
        //         if (powderTypes == null)
        //         {
        //             powderTypes = new();
        // 	        MelonLogger.Msg($"Populating powder types");
        //             foreach (var b in BlueprintManager.Instance.m_AllBlueprints)
        //             {
        //                 if (b.m_RequiredPowder != null)
        //                     foreach (var c in b.m_RequiredPowder)
        //                     {
        //                         if (powderTypes.ContainsKey(c.m_Powder.name)) continue;
        //                         powderTypes.Add(c.m_Powder.name, c.m_Powder);
        //                         MelonModLogger.Msg($"Found PowderType: {c.m_Powder.name}: {Addressables.LoadAsset<LiquidType>(c.m_Powder.name)?.ReferenceCount}");
        //                     }
        //             }

        //             var allLocations = new List<IResourceLocation>();
        //             var resourceLocators = Addressables.ResourceLocators.ToArray();
        //             foreach (var loc in resourceLocators)
        //             {
        // 	            MelonLogger.Msg($"---Locator: {loc.LocatorId}");
        //                 foreach (var k in loc.Keys.ToArray())
        // 	            MelonLogger.Msg($"---{k.GetType().Name} {k.ToString()}");
        //             }

        //         }
        //         return powderTypes;
        //     }
        // }
    }
}