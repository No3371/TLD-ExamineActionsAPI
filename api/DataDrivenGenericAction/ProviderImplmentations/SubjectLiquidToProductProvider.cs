// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Converts the subject's liquid content into a product liquid.
    /// Useful for pouring actions.
    /// ProductLiters = SubjectLiters * LitersScale + LitersOffset.
    /// </summary>
    public class SubjectLiquidToProductProvider : IProductLiquidProvider
	{
		[TinyJSON2.Include]
		public float LitersScale { get; set; }
		[TinyJSON2.Include]
		public float LitersOffset { get; set; }
		public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> Liquid)
		{
			Liquid.Add(new MaterialOrProductLiquidConf(state.Subject.m_LiquidItem.LiquidType, state.Subject.m_LiquidItem.m_Liquid.ToQuantity(LitersScale) + LitersOffset, 100));
		}
	}
}
