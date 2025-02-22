namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubActionIdMappedMaterialLiquidProvider : IMaterialLiquidProvider
    {
        [TinyJSON2.Include]
		public List<List<MaterialOrProductDef>?> LiquidBySubActionId { get; set; }

        public void GetMaterialLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> materials)
        {
            if (LiquidBySubActionId.Count <= state.SubActionId)
                return;

            if (LiquidBySubActionId[state.SubActionId] == null)
                return;

            for (int i = 0; i < LiquidBySubActionId[state.SubActionId].Count; i++)
            {
                var conf = LiquidBySubActionId[state.SubActionId][i].ToLiquidConf(state);
                materials.Add(conf);
            }
        }
    }
}
