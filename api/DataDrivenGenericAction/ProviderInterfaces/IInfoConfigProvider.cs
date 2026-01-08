// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides custom information blocks (e.g. "Required: 500 cal") to be displayed in the action UI.
    /// </summary>
    public interface IInfoConfigProvider
	{
		void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs);
	}
}
