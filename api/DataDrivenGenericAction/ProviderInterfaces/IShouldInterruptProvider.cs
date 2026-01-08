namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines custom conditions that should interrupt the action while it is in progress.
    /// </summary>
    public interface IShouldInterruptProvider
    {
        bool ShouldInterrupt(ExamineActionState state, ref string? message);
    }
}
