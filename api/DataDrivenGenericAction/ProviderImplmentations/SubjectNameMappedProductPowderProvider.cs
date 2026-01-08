
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Maps the subject item's name to a specific powder product definition.
    /// </summary>
    public class SubjectNameMappedProductPowderProvider : IProductPowderProvider
    {
        public SubjectNameMappedProductPowderProvider() {}

        [TinyJSON2.Include]
        public Dictionary<string, MaterialOrProductDef>? Map { get; set; }

        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> products)
        {
            if (Map != null && Map.TryGetValue(state.Subject.name, out var def))
                products.Add(def.ToPowderConf(state));
        }
    }

}
