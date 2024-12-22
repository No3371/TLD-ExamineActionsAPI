namespace ExamineActionsAPI.DataDrivenGenericAction;

public class CallbackChangeSubjectCondition : ICallback
{
    [MelonLoader.TinyJSON.Include]
    public float[] NormalizedConditionChangeBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = NormalizedConditionChangeBySubActionId[state.SubActionId];
        state.Subject.SetNormalizedHP(state.Subject.GetNormalizedCondition() + change);
    }
}