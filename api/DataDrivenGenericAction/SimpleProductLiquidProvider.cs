namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProductLiquidProvider : IProductLiquidProvider
    {
        public SimpleProductLiquidProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductDef>? Liquid { get; set; }
        [MelonLoader.TinyJSON.Include]
        public float[]? LiterOffsetPerSubAction { get; set; }
        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquid)
        {
            for (int i = 0; i < Liquid?.Count; i++)
            {
                var conf = Liquid[i].ToLiquidConf(state);
                if (LiterOffsetPerSubAction != null)
                    conf.Liters += state.SubActionId * LiterOffsetPerSubAction[i];
                liquid.Add(conf);
            }
        }
    }
}
