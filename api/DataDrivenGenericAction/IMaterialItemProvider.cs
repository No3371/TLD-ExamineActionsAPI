// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialItemProvider
	{
		void GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items);
	}
}
