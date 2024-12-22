namespace ExamineActionsAPI.DataDrivenGenericAction;

public class CallbackChangeSubjectCondition : ICallbackProvider
{
    [MelonLoader.TinyJSON.Include]
    public float[] NormalizedConditionChangeBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = NormalizedConditionChangeBySubActionId[state.SubActionId];
        state.Subject.SetNormalizedHP(state.Subject.GetNormalizedCondition() + change);
    }
}