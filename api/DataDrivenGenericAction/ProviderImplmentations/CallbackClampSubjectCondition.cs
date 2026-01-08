using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Clamps the subject item's condition between a minimum and maximum normalized value (0-1).
/// </summary>
public class CallbackClampSubjectCondition : ICallbackProvider
{
    [TinyJSON2.Include]
    public float NormalizedConditionMin { get; set; } = 0;
    public float NormalizedConditionMax { get; set; } = 1;
    public void Run(ExamineActionState state)
    {
        state.Subject.SetNormalizedHP(Mathf.Clamp(state.Subject.GetNormalizedCondition(), NormalizedConditionMin, NormalizedConditionMax));
    }
}
