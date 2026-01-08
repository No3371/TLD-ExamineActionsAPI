namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines the number of sub-actions (variations or quantity options) available for this action.
    /// </summary>
    public interface ISubActionCountProvider
    {
        /// <returns>Number of sub-actions (default is usually 1).</returns>
        int GetSubActionCount (ExamineActionState state);
    }
}
