namespace ExamineActionsAPI.DataDrivenGenericAction;

public class CallbackSetSubjectCondition : ICallbackProvider
{
    [MelonLoader.TinyJSON.Include]
    public float[] NormalizedConditionBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = NormalizedConditionBySubActionId[state.SubActionId];
        state.Subject.SetNormalizedHP(change);
    }
}