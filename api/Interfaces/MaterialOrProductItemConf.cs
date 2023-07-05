namespace ExamineActionsAPI
{
    public struct MaterialOrProductItemConf
	{
        public MaterialOrProductItemConf(string gearName, int units, byte chance)
        {
            GearName = gearName;
            Units = units;
            Chance = chance;
        }

        public string GearName { get; set; }
		public int Units { get; set; }
		public byte Chance { get; set; }
	}
}
