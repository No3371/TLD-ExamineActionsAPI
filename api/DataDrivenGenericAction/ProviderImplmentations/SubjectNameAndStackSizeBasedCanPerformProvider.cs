// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SubjectNameAndStackSizeBasedCanPerformProvider : ICanPerformProvider
{
	[TinyJSON2.Include]
	int DefaultStackSizeRequired { get; set; } = 99999;
	[TinyJSON2.Include]
	public Dictionary<string, int> Map { get; set;}

    public bool CanPerform(ExamineActionState state)
    {
		var item = state.Subject;
		int stackSize = item.m_StackableItem?.m_Units ?? 1;

		if (Map.TryGetValue(item.name, out var req))
			return stackSize >= req;
		else
			return stackSize >= DefaultStackSizeRequired;
    }
}