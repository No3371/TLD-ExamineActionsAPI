
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProductItemProvider : IProductItemProvider
    {
        public SimpleProductItemProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Items { get; set; }
        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            for (int i = 0; i < Items?.Count; i++)
            {
                var conf = Items[i].ToItemConf(state);
                products.Add(conf);
            }
        }
    }
}
