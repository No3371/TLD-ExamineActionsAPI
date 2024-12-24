// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

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
