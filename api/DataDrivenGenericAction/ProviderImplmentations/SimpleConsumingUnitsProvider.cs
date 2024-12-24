namespace ExamineActionsAPI.DataDrivenGenericAction
{
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
