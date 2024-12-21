// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialPowderProvider
	{
		void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> materials);
	}
}
