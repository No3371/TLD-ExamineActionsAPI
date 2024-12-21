
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectNameMappedProductItemProvider : IProductItemProvider
    {
        public SubjectNameMappedProductItemProvider() {}
        [MelonLoader.TinyJSON.Include]
        public Dictionary<string, MaterialOrProductDef>? Map { get; set; }
        
        public void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products)
        {
            if (Map != null && Map.TryGetValue(state.Subject.name, out var def))
            {
                MaterialOrProductItemConf item = def.ToItemConf(state);
                products.Add(item);
            }
        }
    }

}
