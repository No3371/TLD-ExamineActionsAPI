// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialItemProvider
	{
		void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> materials);
	}
}
