// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns true if any of the provided IIsActionAvailableProvider instances returns true.
/// Essentially performs a logical OR operation on the results of the sub-providers.
/// </summary>
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