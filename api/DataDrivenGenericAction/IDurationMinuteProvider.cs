// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IDurationMinutesProvider
	{
		int CalculateDurationMinutes(ExamineActionState state);
	}
}
