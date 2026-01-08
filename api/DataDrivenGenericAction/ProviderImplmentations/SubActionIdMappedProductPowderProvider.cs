
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides powder products based on the current sub-action ID.
    /// Index matches SubActionId.
    /// </summary>
    public class SubActionIdMappedProductPowderProvider : IProductPowderProvider
    {
        [TinyJSON2.Include]
		public List<List<MaterialOrProductDef>?> PowderBySubActionId { get; set; }

        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> products)
        {
            if (PowderBySubActionId.Count <= state.SubActionId)
                return;

            if (PowderBySubActionId[state.SubActionId] == null)
                return;

            for (int i = 0; i < PowderBySubActionId[state.SubActionId].Count; i++)
            {
                var conf = PowderBySubActionId[state.SubActionId][i].ToPowderConf(state);
                products.Add(conf);
            }
        }
    }
}
