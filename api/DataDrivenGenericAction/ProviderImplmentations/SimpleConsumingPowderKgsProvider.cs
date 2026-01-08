namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Calculates the amount of powder to consume in kilograms.
/// ConsumingPowderKgs = BaseConsumingPowderKgs + (SubActionId * ConsumingPowderKgsOffsetPerSubAction)
/// </summary>
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
