namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleGetConsumingLiquidLitersProvider : IGetConsumingLiquidLitersProvider
{
    [MelonLoader.TinyJSON.Include]
		public float BaseConsumingLiquidLiters { get; set; }
    [MelonLoader.TinyJSON.Include]
		public float ConsumingLiquidLitersOffsetPerSubAction { get; set; }

    float IGetConsumingLiquidLitersProvider.GetConsumingLiquidLiters(ExamineActionState state)
    {
        return BaseConsumingLiquidLiters + state.SubActionId * ConsumingLiquidLitersOffsetPerSubAction;
    }
}
