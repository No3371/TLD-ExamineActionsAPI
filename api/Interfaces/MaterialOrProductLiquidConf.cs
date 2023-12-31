using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public struct MaterialOrProductLiquidConf
	{
        public MaterialOrProductLiquidConf(LiquidType type, float liters, byte chance)
        {
            Type = type;
            Liters = liters;
            Chance = chance;
        }

        public LiquidType Type { get; set; }
		public float Liters { get; set; }
		public byte Chance { get; set; }
	}
}
