// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IMaterialLiquidProvider
	{
		void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> materials);
	}
}
