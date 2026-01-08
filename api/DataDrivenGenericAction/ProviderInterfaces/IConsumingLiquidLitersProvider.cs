namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines how many liters of liquid are consumed from the subject item.
    /// </summary>
    public interface IConsumingLiquidLitersProvider
    {
        float GetConsumingLiquidLiters(ExamineActionState state);
    }
}
