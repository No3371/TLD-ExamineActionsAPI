// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface ICanPerformProvider
	{
		bool CanPerform(ExamineActionState state);
	}
}
