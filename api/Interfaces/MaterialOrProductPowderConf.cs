using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public struct MaterialOrProductPowderConf
	{
        public MaterialOrProductPowderConf(PowderType type, float kgs, byte chance)
        {
            Type = type;
            Kgs = kgs;
            Chance = chance;
        }

        public PowderType Type { get; set; }
		public float Kgs { get; set; }
		public byte Chance { get; set; }
	}
}
