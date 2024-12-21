
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMultiProductItemProvider : IProductItemProvider
    {
        [MelonLoader.TinyJSON.Include]
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
