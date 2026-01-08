
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides a list of item products to generate.
    /// Supports scaling quantities based on sub-action ID.
    /// </summary>
    public class SimpleProductItemProvider : IProductItemProvider
    {
        public SimpleProductItemProvider() {}

        [TinyJSON2.Include]
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
