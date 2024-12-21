
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleSingleMaterialPowderProvider : IMaterialPowderProvider
    {
        public SimpleSingleMaterialPowderProvider() {}
        [MelonLoader.TinyJSON.Include]
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
