// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

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