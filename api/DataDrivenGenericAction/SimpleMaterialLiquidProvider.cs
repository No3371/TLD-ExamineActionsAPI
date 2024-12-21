using System.Text.Json.Serialization;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMaterialLiquidProvider : IMaterialLiquidProvider
    {
        public SimpleMaterialLiquidProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Liquid { get; set; }
        public void GetRequiredLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquid)
        {
            for (int i = 0; i < Liquid?.Count; i++)
            {
                var conf = Liquid[i].ToLiquidConf(state);
                liquid.Add(conf);
            }
        }
    }
}
