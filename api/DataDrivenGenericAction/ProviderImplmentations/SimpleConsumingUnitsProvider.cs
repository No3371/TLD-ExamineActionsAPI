namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleConsumingUnitsProvider : IConsumingUnitsProvider
    {
        [MelonLoader.TinyJSON.Include]
		public int BaseConsumingUnits { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int ConsumingUnitsOffsetPerSubAction { get; set; }
        public int GetConsumingUnits(ExamineActionState state)
        {
            return BaseConsumingUnits + state.SubActionId * ConsumingUnitsOffsetPerSubAction;
        }
    }
}
