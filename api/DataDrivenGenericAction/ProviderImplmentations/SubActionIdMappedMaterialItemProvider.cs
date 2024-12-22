// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubActionIdMappedMaterialItemProvider : IMaterialItemProvider
    {
        [MelonLoader.TinyJSON.Include]
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
