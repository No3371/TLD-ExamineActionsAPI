
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProductItemProvider : IProductItemProvider
    {
        public SimpleProductItemProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductDef>? Items { get; set; }
        [MelonLoader.TinyJSON.Include]
        public int[]? UnitOffsetPerSubAction { get; set; }
        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            for (int i = 0; i < Items?.Count; i++)
            {
                var conf = Items[i].ToItemConf(state);
                if (UnitOffsetPerSubAction != null)
                    conf.Units += state.SubActionId * UnitOffsetPerSubAction[i];
                products.Add(conf);
            }
        }
    }

}
