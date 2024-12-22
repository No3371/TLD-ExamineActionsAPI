namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProductLiquidProvider : IProductLiquidProvider
    {
        public SimpleProductLiquidProvider() {}
        [MelonLoader.TinyJSON.Include]
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
