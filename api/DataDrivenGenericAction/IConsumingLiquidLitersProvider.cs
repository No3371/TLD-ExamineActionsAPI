namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IConsumingLiquidLitersProvider
    {
        float GetConsumingLiquidLiters(ExamineActionState state);
    }
}
