namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Calculates the number of units to consume from the subject item.
    /// ConsumingUnits = BaseConsumingUnits + (SubActionId * ConsumingUnitsOffsetPerSubAction)
    /// </summary>
    public class SimpleConsumingUnitsProvider : IConsumingUnitsProvider
    {
        [TinyJSON2.Include]
		public int BaseConsumingUnits { get; set; }
        [TinyJSON2.Include]
		public int ConsumingUnitsOffsetPerSubAction { get; set; }
        public int GetConsumingUnits(ExamineActionState state)
        {
            return BaseConsumingUnits + state.SubActionId * ConsumingUnitsOffsetPerSubAction;
        }
    }
}
