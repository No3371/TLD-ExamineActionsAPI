// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialLiquidProvider
	{
		void GetRequiredLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> Liquid);
	}
}
