
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectNameMappedProductLiquidProvider : IProductLiquidProvider
    {
        public SubjectNameMappedProductLiquidProvider() {}
        [MelonLoader.TinyJSON.Include]
        public Dictionary<string, MaterialOrProductDef>? Map { get; set; }

        public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> products)
        {
            if (Map != null && Map.TryGetValue(state.Subject.name, out var def))
                products.Add(def.ToLiquidConf(state));
        }
    }

}
