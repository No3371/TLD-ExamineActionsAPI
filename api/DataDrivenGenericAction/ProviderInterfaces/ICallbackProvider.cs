namespace ExamineActionsAPI.DataDrivenGenericAction;

public interface ICallbackProvider
{
    void Run (ExamineActionState state);
}
