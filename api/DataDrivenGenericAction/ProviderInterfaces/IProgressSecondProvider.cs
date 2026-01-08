// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Determines the real-time duration of the action (how long the progress bar takes).
    /// </summary>
    public interface IProgressSecondProvider
	{
		float GetProgressSeconds(ExamineActionState state);
	}
}
