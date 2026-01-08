namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides a list of liquid products to generate.
    /// Supports scaling quantities based on sub-action ID via MaterialOrProductSizedBySubActionDef.
    /// </summary>
    public class SimpleProductLiquidProvider : IProductLiquidProvider
    {
        public SimpleProductLiquidProvider() {}

        [TinyJSON2.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Liquid { get; set; }
        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> products)
        {
            for (int i = 0; i < Liquid?.Count; i++)
            {
                var conf = Liquid[i].ToLiquidConf(state);
                products.Add(conf);
            }
        }
    }
}
