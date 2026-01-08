// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IInfoConfigProvider
	{
		void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs);
	}
}
