// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns true if all of the provided IIsActionAvailableProvider instances return true.
/// Essentially performs a logical AND operation on the results of the sub-providers.
/// </summary>
public class LogicAndIsActionAvailableProvider : IIsActionAvailableProvider
{
	[TinyJSON2.Include]
	public IIsActionAvailableProvider[] Providers { get; set; }
    public bool IsActionAvailable(GearItem item)
    {
			foreach (var provider in Providers)
				if (!provider.IsActionAvailable(item))
					return false;
			return true;
    }
}
