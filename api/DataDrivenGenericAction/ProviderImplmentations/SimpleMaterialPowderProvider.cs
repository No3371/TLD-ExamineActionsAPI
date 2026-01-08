
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// The provider simply requires the specified powders as material.
    /// Supports scaling quantities based on sub-action ID.
    /// </summary>
    public class SimpleMaterialPowderProvider : IMaterialPowderProvider
    {
        public SimpleMaterialPowderProvider() {}

        [TinyJSON2.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Powder { get; set; }
        public void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> materials)
        {
            for (int i = 0; i < Powder?.Count; i++)
            {
                var conf = Powder[i].ToPowderConf(state);
                materials.Add(conf);
            }
        }
    }
}
