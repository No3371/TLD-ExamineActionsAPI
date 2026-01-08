// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines if the action can currently be performed based on custom conditions (e.g. subject condition, player status).
    /// </summary>
    public interface ICanPerformProvider
	{
		bool CanPerform(ExamineActionState state);
	}
}
