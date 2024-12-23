namespace ExamineActionsAPI.DataDrivenGenericAction;

public class CallbackChangeSubjectCondition : ICallbackProvider
{
    [MelonLoader.TinyJSON.Include]
    public float BaseNormalizedConditionChange { get; set; }
    [MelonLoader.TinyJSON.Include]
    public float NormalizedConditionChangeOffsetBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = BaseNormalizedConditionChange + NormalizedConditionChangeOffsetBySubActionId * state.SubActionId;
        state.Subject.SetNormalizedHP(state.Subject.GetNormalizedCondition() + change);
    }
}
