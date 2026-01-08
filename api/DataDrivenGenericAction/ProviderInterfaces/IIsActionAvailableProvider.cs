// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines if the action is available for a specific gear item (whether the menu button appears).
    /// </summary>
    public interface IIsActionAvailableProvider
	{
		bool IsActionAvailable(GearItem item);
	}
}
