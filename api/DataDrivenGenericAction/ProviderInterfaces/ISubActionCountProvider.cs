namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface ISubActionCountProvider
    {
        int GetSubActionCount (ExamineActionState state);
    }
}
