
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleMultiMaterialPowderProvider : IMaterialPowderProvider
    {
        [MelonLoader.TinyJSON.Include]
		public List<List<MaterialOrProductDef>?> PowderBySubActionId { get; set; }

        public void GetMaterialPowder(ExamineActionState state, List<MaterialOrProductPowderConf> materials)
        {
            if (PowderBySubActionId.Count <= state.SubActionId)
                return;

            if (PowderBySubActionId[state.SubActionId] == null)
                return;

            for (int i = 0; i < PowderBySubActionId[state.SubActionId].Count; i++)
            {
                var conf = PowderBySubActionId[state.SubActionId][i].ToPowderConf(state);
                materials.Add(conf);
            }
        }
    }
}
