
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleSingleProductPowderProvider : IProductPowderProvider
    {
        public SimpleSingleProductPowderProvider() {}
        [MelonLoader.TinyJSON.Include]
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
