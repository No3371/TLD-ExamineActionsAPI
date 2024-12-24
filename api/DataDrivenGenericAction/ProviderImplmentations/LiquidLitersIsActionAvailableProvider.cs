// #define VERY_VERBOSE
using Il2Cpp;
using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class LiquidLitersIsActionAvailableProvider : IIsActionAvailableProvider
    {
        [Include]
        public List<string>? ValidLiquidTypes { get; set; }
        [Include]
        public float LitersRequired { get; set; }
        public bool IsActionAvailable(GearItem item)
        {
            var m_LiquidItem = item.m_LiquidItem;
            if (m_LiquidItem == null
             || m_LiquidItem.m_Liquid.ToQuantity(1) < LitersRequired
             ||(ValidLiquidTypes != null && !ValidLiquidTypes.Contains(m_LiquidItem.m_LiquidType.name)))
                return false;

            return true;
        }
    }

}
