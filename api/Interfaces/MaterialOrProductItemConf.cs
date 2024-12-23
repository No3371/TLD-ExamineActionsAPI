namespace ExamineActionsAPI
{
    public struct MaterialOrProductItemConf
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gearName">GEAR_NNN</param>
        /// <param name="chance">0 ~ 100</param>
        public MaterialOrProductItemConf(string gearName, int units, float chance)
        {
            GearName = gearName;
            Units = units;
            Chance = chance;
        }

        /// <value>GEAR_NNN</value>
        public string GearName { get; set; }
		public int Units { get; set; }
        /// <summary>
        /// Chance to consume/yield
        /// </summary>
        /// <value>0-100</value>
		public float Chance { get; set; }
	}
}
