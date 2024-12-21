// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleSingleMaterialItemProvider : IMaterialItemProvider
    {
        [MelonLoader.TinyJSON.Include]
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
