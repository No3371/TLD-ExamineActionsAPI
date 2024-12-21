// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectLiquidToProductProvider : IProductLiquidProvider
	{
		[MelonLoader.TinyJSON.Include]
		public float LitersScale { get; set; }
		[MelonLoader.TinyJSON.Include]
		public float LitersOffset { get; set; }
		public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> Liquid)
		{
			Liquid.Add(new MaterialOrProductLiquidConf(state.Subject.m_LiquidItem.LiquidType, state.Subject.m_LiquidItem.m_Liquid.ToQuantity(LitersScale) + LitersOffset, 100));
		}
	}
}
