// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the powder produced by the action.
    /// </summary>
    public interface IProductPowderProvider
	{
		void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> products);
	}
}
