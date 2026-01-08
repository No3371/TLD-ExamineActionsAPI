namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Calculates the amount of liquid to consume in liters.
/// ConsumingLiquidLiters = BaseConsumingLiquidLiters + (SubActionId * ConsumingLiquidLitersOffsetPerSubAction)
/// </summary>
public class SimpleConsumingLiquidLitersProvider : IConsumingLiquidLitersProvider
{
    [TinyJSON2.Include]
		public float BaseConsumingLiquidLiters { get; set; }
    [TinyJSON2.Include]
		public float ConsumingLiquidLitersOffsetPerSubAction { get; set; }

    float IConsumingLiquidLitersProvider.GetConsumingLiquidLiters(ExamineActionState state)
    {
        return BaseConsumingLiquidLiters + state.SubActionId * ConsumingLiquidLitersOffsetPerSubAction;
    }
}
