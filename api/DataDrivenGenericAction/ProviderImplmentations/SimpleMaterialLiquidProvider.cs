using System.Text.Json.Serialization;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMaterialLiquidProvider : IMaterialLiquidProvider
    {
        public SimpleMaterialLiquidProvider() {}
        [TinyJSON2.Include]
        public List<MaterialOrProductSizedBySubActionDef>? Liquid { get; set; }
        public void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> materials)
        {
            for (int i = 0; i < Liquid?.Count; i++)
            {
                var conf = Liquid[i].ToLiquidConf(state);
                materials.Add(conf);
            }
        }
    }
}
