// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Requires item materials based on the current sub-action ID.
    /// Index matches SubActionId.
    /// </summary>
    public class SubActionIdMappedMaterialItemProvider : IMaterialItemProvider
    {
        [TinyJSON2.Include]
		public List<List<MaterialOrProductDef>?> ItemBySubActionId { get; set; }

        public void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> materials)
        {
            if (ItemBySubActionId.Count <= state.SubActionId)
                return;

            if (ItemBySubActionId[state.SubActionId] == null)
                return;

            for (int i = 0; i < ItemBySubActionId[state.SubActionId].Count; i++)
            {
                var conf = ItemBySubActionId[state.SubActionId][i].ToItemConf(state);
                materials.Add(conf);
            }
        }
    }
}
