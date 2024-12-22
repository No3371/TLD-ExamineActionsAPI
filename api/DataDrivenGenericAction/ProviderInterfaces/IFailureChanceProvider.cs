// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IFailureChanceProvider
	{
		float GetFailureChance(ExamineActionState state);
	}
}
