// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Returns the opposite of the provided IIsActionAvailableProvider instance.
/// Essentially performs a logical NOT operation on the result of the sub-provider.
/// </summary>
public class LogicNotIsActionAvailableProvider : IIsActionAvailableProvider
{
	[TinyJSON2.Include]
	public IIsActionAvailableProvider Provider { get; set; }
	public bool IsActionAvailable(GearItem item)
	{
		return !Provider.IsActionAvailable(item);
	}
}