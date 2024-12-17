namespace ExamineActionsAPI
{
    public struct MaterialOrProductItemConf
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gearName">GEAR_NNN</param>
        /// <param name="chance">0 ~ 100</param>
        public MaterialOrProductItemConf(string gearName, int units, byte chance)
        {
            GearName = gearName;
            Units = units;
            Chance = chance;
        }

        /// <value>GEAR_NNN</value>
        public string GearName { get; set; }
		public int Units { get; set; }
        /// <summary>
        /// 0-100% chance to consume/yield
        /// </summary>
		public byte Chance { get; set; }
	}
}
