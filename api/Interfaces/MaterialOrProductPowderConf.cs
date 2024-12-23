using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public struct MaterialOrProductPowderConf
	{
        public MaterialOrProductPowderConf(PowderType type, float kgs, float chance)
        {
            Type = type;
            Kgs = kgs;
            Chance = chance;
        }

        public PowderType Type { get; set; }
		public float Kgs { get; set; }
        /// <summary>
        /// Chance to consume/yield
        /// </summary>
        /// <value>0-100</value>
		public float Chance { get; set; }
	}
}
