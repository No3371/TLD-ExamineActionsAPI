// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

public class LogicOrIsActionAvailableProvider : IIsActionAvailableProvider
{
	[TinyJSON2.Include]
	public IIsActionAvailableProvider[] Providers { get; set; }
    public bool IsActionAvailable(GearItem item)
    {
        foreach (var provider in Providers)
            if (provider.IsActionAvailable(item))
                return true;
        return false;
    }
}