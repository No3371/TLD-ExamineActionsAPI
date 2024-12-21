namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleConsumingPowderKgsProvider : IConsumingPowderKgsProvider
{
    [MelonLoader.TinyJSON.Include]
		public float BaseConsumingPowderKgs { get; set; }
    [MelonLoader.TinyJSON.Include]
		public float ConsumingPowderKgsOffsetPerSubAction { get; set; }

    float IConsumingPowderKgsProvider.GetConsumingPowderKgs(ExamineActionState state)
    {
        return BaseConsumingPowderKgs + state.SubActionId * ConsumingPowderKgsOffsetPerSubAction;
    }
}
