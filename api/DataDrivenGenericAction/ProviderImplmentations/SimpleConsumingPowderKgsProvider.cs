namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleConsumingPowderKgsProvider : IConsumingPowderKgsProvider
{
    [TinyJSON2.Include]
		public float BaseConsumingPowderKgs { get; set; }
    [TinyJSON2.Include]
		public float ConsumingPowderKgsOffsetPerSubAction { get; set; }

    float IConsumingPowderKgsProvider.GetConsumingPowderKgs(ExamineActionState state)
    {
        return BaseConsumingPowderKgs + state.SubActionId * ConsumingPowderKgsOffsetPerSubAction;
    }
}
