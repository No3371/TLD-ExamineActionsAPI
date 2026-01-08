namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Provides liquid products based on the current sub-action ID.
/// Index matches SubActionId.
/// </summary>
public class SubActionIdMappedProductLiquidProvider : IProductLiquidProvider
{
    [TinyJSON2.Include]
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
