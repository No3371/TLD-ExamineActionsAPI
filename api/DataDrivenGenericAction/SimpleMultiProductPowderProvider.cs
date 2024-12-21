
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMultiProductPowderProvider : IProductPowderProvider
    {
        [MelonLoader.TinyJSON.Include]
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
