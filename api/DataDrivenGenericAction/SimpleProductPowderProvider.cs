
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProductPowderProvider : IProductPowderProvider
    {
        public SimpleProductPowderProvider() {}
        [MelonLoader.TinyJSON.Include]
        public List<MaterialOrProductDef>? Powder { get; set; }
        [MelonLoader.TinyJSON.Include]
        public float[]? KGOffsetPerSubAction { get; set; }
        public void GetProductPowder(ExamineActionState state, List<MaterialOrProductPowderConf> powder)
        {
            for (int i = 0; i < Powder?.Count; i++)
            {
                var conf = Powder[i].ToPowderConf(state);
                if (KGOffsetPerSubAction != null)
                    conf.Kgs += state.SubActionId * KGOffsetPerSubAction[i];
                powder.Add(conf);
            }
        }
    }
}
