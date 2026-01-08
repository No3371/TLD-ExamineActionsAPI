namespace ExamineActionsAPI.DataDrivenGenericAction;

public class CallbackChangeSubjectCondition : ICallbackProvider
{
    [TinyJSON2.Include]
    public float BaseNormalizedConditionChange { get; set; }
    [TinyJSON2.Include]
    public float NormalizedConditionChangeOffsetBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = BaseNormalizedConditionChange + NormalizedConditionChangeOffsetBySubActionId * state.SubActionId;
        state.Subject!.SetNormalizedHP(state.Subject.GetNormalizedCondition() + change);
    }
}
