// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IFailureChanceProvider
	{
		float CalculateFailureChance(ExamineActionState state);
	}
}
