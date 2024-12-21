// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IProductLiquidProvider
	{
		void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> products);
	}
}
