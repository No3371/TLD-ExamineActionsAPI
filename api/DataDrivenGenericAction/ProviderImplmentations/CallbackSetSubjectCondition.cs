namespace ExamineActionsAPI.DataDrivenGenericAction;

public partial class CallbackSetSubjectCondition : ICallbackProvider
{
    [TinyJSON2.Include]
    public float[] NormalizedConditionBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = NormalizedConditionBySubActionId[state.SubActionId];
        state.Subject.SetNormalizedHP(change);
    }
}