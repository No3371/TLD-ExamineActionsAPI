// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns the opposite of the provided ICanPerformProvider instance.
/// Essentially performs a logical NOT operation on the result of the sub-provider.
/// </summary>
public class LogicNotCanPerformProvider : ICanPerformProvider
{
    [TinyJSON2.Include]
    public ICanPerformProvider Provider { get; set; }
    public bool CanPerform(ExamineActionState state)
    {
        return !Provider.CanPerform(state);
    }
}