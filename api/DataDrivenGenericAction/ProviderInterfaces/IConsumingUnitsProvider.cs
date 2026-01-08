namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines how many units (of a stackable item) are consumed from the subject item.
    /// </summary>
    public interface IConsumingUnitsProvider
    {
        int GetConsumingUnits(ExamineActionState state);
    }
}
