// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns true if any of the provided ICanPerformProvider instances returns true.
/// Essentially performs a logical OR operation on the results of the sub-providers.
/// </summary>
public class LogicOrCanPerformProvider : ICanPerformProvider
{
    [TinyJSON2.Include]
    public ICanPerformProvider[] Providers { get; set; }
    public bool CanPerform(ExamineActionState state)
    {
        foreach (var provider in Providers)
            if (provider.CanPerform(state))
                return true;
        return false;
    }
}
