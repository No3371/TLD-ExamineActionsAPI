// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the liquid required (and consumed) as material for the action.
    /// </summary>
    public interface IMaterialLiquidProvider
	{
		void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> materials);
	}
}
