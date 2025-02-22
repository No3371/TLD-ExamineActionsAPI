// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMaterialItemProvider : IMaterialItemProvider
    {
        [TinyJSON2.Include]
		public List<MaterialOrProductSizedBySubActionDef> Item { get; set; }

        public void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> materials)
        {
            for (int i = 0; i < Item?.Count; i++)
            {
                var conf = Item[i].ToItemConf(state);
                materials.Add(conf);
            }
        }
    }
}
