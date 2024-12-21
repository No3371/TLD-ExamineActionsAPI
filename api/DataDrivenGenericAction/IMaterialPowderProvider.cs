// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialPowderProvider
	{
		void GetRequiredPowder(ExamineActionState state, List<MaterialOrProductPowderConf> Powder);
	}
}
