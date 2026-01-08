// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the liquid produced by the action.
    /// </summary>
    public interface IProductLiquidProvider
	{
		void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> products);
	}
}
