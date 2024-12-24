
namespace ExamineActionsAPI.DataDrivenGenericAction
{
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
