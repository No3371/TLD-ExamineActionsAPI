namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleGetConsumingPowderKgsProvider : IGetConsumingPowderKgsProvider
{
    [MelonLoader.TinyJSON.Include]
		public float BaseConsumingPowderKgs { get; set; }
    [MelonLoader.TinyJSON.Include]
		public float ConsumingPowderKgsOffsetPerSubAction { get; set; }

    float IGetConsumingPowderKgsProvider.GetConsumingPowderKgs(ExamineActionState state)
    {
        return BaseConsumingPowderKgs + state.SubActionId * ConsumingPowderKgsOffsetPerSubAction;
    }
}
