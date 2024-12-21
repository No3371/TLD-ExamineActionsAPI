// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMaterialItemProvider : IMaterialItemProvider
    {
        public SimpleMaterialItemProvider() {}
        [MelonLoader.TinyJSON.Include]
		public List<MaterialOrProductDef>? Items { get; set; }
        [MelonLoader.TinyJSON.Include]
        public int[]? UnitOffsetPerSubAction { get; set; }

        public SimpleMaterialItemProvider(params MaterialOrProductDef[] items)
        {
            Items = new (items);
        }

        public void GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items)
        {
            for (int i = 0; i < Items?.Count; i++)
            {
                var conf = Items[i].ToItemConf(state);
                if (UnitOffsetPerSubAction != null && UnitOffsetPerSubAction.Length > i)
                    conf.Units += state.SubActionId * UnitOffsetPerSubAction[i];
                items.Add(conf);
            }
        }
    }
}
