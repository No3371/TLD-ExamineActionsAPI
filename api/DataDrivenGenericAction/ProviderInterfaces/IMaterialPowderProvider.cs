// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the powder required (and consumed) as material for the action.
    /// </summary>
    public interface IMaterialPowderProvider
	{
		void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> materials);
	}
}
