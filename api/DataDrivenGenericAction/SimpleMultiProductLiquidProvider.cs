namespace ExamineActionsAPI.DataDrivenGenericAction;

public class SimpleMultiProductLiquidProvider : IProductLiquidProvider
{
    [MelonLoader.TinyJSON.Include]
		public List<List<MaterialOrProductDef>?> LiquidBySubActionId { get; set; }

    public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> products)
    {
        if (LiquidBySubActionId.Count <= state.SubActionId)
            return;

        if (LiquidBySubActionId[state.SubActionId] == null)
            return;

        for (int i = 0; i < LiquidBySubActionId[state.SubActionId].Count; i++)
        {
            var conf = LiquidBySubActionId[state.SubActionId][i].ToLiquidConf(state);
            products.Add(conf);
        }
    }
}
