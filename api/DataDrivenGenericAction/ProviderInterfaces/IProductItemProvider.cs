// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IProductItemProvider
	{
		void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products);
	}
}
