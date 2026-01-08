namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines how many kilograms of powder are consumed from the subject item.
    /// </summary>
    public interface IConsumingPowderKgsProvider
    {
        float GetConsumingPowderKgs(ExamineActionState state);
    }
}
