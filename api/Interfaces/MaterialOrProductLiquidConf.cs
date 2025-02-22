using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public struct MaterialOrProductLiquidConf
	{
        public MaterialOrProductLiquidConf(LiquidType type, float liters, float chance)
        {
            Type = type;
            Liters = liters;
            Chance = chance;
        }

        public LiquidType Type { get; set; }
		public float Liters { get; set; }
        /// <summary>
        /// 0-100% chance to consume/yield
        /// </summary>
		public float Chance { get; set; }
	}
}
