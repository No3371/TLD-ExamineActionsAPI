// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SubjectNameAndStackSizeBasedIsActionAvailableProvider : IIsActionAvailableProvider
{
		[TinyJSON2.Include]
		int DefaultStackSizeRequired { get; set; } = 99999;
		[TinyJSON2.Include]
		public Dictionary<string, int> Map { get; set;}

    public bool IsActionAvailable(GearItem item)
    {
			int stackSize = item.m_StackableItem?.m_Units ?? 1;

			if (Map.TryGetValue(item.name, out var req))
				return stackSize >= req;
			else
				return stackSize >= DefaultStackSizeRequired;
    }
}
