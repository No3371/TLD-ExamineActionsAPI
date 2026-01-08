// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Determines the duration of the action in in-game minutes.
/// </summary>
public interface IDurationMinutesProvider
{
	int GetDurationMinutes(ExamineActionState state);
}
