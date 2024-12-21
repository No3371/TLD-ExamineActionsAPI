namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleConsumingLiquidLitersProvider : IConsumingLiquidLitersProvider
{
    [MelonLoader.TinyJSON.Include]
		public float BaseConsumingLiquidLiters { get; set; }
    [MelonLoader.TinyJSON.Include]
		public float ConsumingLiquidLitersOffsetPerSubAction { get; set; }

    float IConsumingLiquidLitersProvider.GetConsumingLiquidLiters(ExamineActionState state)
    {
        return BaseConsumingLiquidLiters + state.SubActionId * ConsumingLiquidLitersOffsetPerSubAction;
    }
}
