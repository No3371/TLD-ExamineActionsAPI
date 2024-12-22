// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IProductPowderProvider
	{
		void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> products);
	}
}
