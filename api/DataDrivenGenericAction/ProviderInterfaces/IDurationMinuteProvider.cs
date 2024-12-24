// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

public interface IDurationMinutesProvider
{
	int GetDurationMinutes(ExamineActionState state);
}
