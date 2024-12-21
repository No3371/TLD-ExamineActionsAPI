// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleSingleMaterialItemProvider : IMaterialItemProvider
    {
        [MelonLoader.TinyJSON.Include]
		public List<MaterialOrProductSizedBySubActionDef> Items { get; set; }

        public void GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
            for (int i = 0; i < Items?.Count; i++)
            {
                var conf = Items[i].ToItemConf(state);
                items.Add(conf);
            }
        }
    }
}
