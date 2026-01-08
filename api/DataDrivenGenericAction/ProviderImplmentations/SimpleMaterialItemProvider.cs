// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// The provider simply requires the specified items as material
    /// </summary>
    public class SimpleMaterialItemProvider : IMaterialItemProvider
    {
        [TinyJSON2.Include]
		public List<MaterialOrProductSizedBySubActionDef>? Item { get; set; }

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
