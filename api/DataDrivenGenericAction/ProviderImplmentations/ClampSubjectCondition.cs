using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction;

public class ClampSubjectCondition : ICallbackProvider
{
    [TinyJSON2.Include]
    public float NormalizedConditionMin { get; set; } = 0;
    public float NormalizedConditionMax { get; set; } = 1;
    public void Run(ExamineActionState state)
    {
        state.Subject.SetNormalizedHP(Mathf.Clamp(state.Subject.GetNormalizedCondition(), NormalizedConditionMin, NormalizedConditionMax));
    }
}
