// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IFailureChanceProvider
	{
        /// <returns>Failure chance (0.0 to 100.0).</returns>
		float GetFailureChance(ExamineActionState state);
	}
}
