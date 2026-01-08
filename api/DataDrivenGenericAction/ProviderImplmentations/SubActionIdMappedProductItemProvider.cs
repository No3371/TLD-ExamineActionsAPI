
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides item products based on the current sub-action ID.
    /// Index matches SubActionId.
    /// </summary>
    public class SubActionIdMappedProductItemProvider : IProductItemProvider
    {
        [TinyJSON2.Include]
		public List<List<MaterialOrProductDef>?> ItemBySubActionId { get; set; }
        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            if (ItemBySubActionId.Count <= state.SubActionId)
                return;

            if (ItemBySubActionId[state.SubActionId] == null)
                return;

            for (int i = 0; i < ItemBySubActionId[state.SubActionId].Count; i++)
            {
                var conf = ItemBySubActionId[state.SubActionId][i].ToItemConf(state);
                products.Add(conf);
            }
        }
    }
}
