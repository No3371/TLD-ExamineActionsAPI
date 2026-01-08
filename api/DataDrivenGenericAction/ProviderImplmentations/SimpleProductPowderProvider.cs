
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides a list of powder products to generate.
    /// </summary>
    public class SimpleProductPowderProvider : IProductPowderProvider
    {
        public SimpleProductPowderProvider() {}

        [TinyJSON2.Include]
        public List<MaterialOrProductDef>? Powder { get; set; }
        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> products)
        {
            for (int i = 0; i < Powder?.Count; i++)
            {
                var conf = Powder[i].ToPowderConf(state);
                products.Add(conf);
            }
        }
    }
}
