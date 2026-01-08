// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the items produced by the action.
    /// </summary>
    public interface IProductItemProvider
	{
		void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products);
	}
}
