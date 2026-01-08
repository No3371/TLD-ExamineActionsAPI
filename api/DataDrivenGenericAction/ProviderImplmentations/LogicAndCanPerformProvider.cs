// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns true if all of the provided ICanPerformProvider instances return true.
/// Essentially performs a logical AND operation on the results of the sub-providers.
/// </summary>
public class LogicAndCanPerformProvider : ICanPerformProvider
{
    [TinyJSON2.Include]
    public ICanPerformProvider[] Providers { get; set; }
    public bool CanPerform(ExamineActionState state)
    {
        foreach (var provider in Providers)
            if (!provider.CanPerform(state))
                return false;
        return true;
    }
}
