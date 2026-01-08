namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Sets the subject item's condition to a specific value based on the current sub-action ID.
/// </summary>
public class CallbackSetSubjectCondition : ICallbackProvider
{
    [TinyJSON2.Include]
    public float[] NormalizedConditionBySubActionId { get; set; }
    public void Run(ExamineActionState state)
    {
        var change = NormalizedConditionBySubActionId[state.SubActionId];
        state.Subject.SetNormalizedHP(change);
    }
}