// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the items required (and consumed) as materials for the action.
    /// </summary>
    public interface IMaterialItemProvider
	{
		void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> materials);
	}
}
