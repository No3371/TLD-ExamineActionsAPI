namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Defines a callback to be executed at specific lifecycle events of the action (e.g., OnSuccess, OnFailure).
/// </summary>
public interface ICallbackProvider
{
    void Run (ExamineActionState state);
}
