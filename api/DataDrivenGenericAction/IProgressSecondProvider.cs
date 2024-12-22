// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IProgressSecondProvider
	{
		float GetProgressSeconds(ExamineActionState state);
	}
}
