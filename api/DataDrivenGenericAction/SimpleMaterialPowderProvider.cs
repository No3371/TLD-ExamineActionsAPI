
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMaterialPowderProvider : IMaterialPowderProvider
    {
        public SimpleMaterialPowderProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Powder { get; set; }
        public void GetRequiredPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powder)
        {
            for (int i = 0; i < Powder?.Count; i++)
            {
                var conf = Powder[i].ToPowderConf(state);
                powder.Add(conf);
            }
        }
    }
}
