// #define VERY_VERBOSE
using Il2Cpp;
using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class PowderKgsIsActionAvailableProvider : IIsActionAvailableProvider
    {
        [Include]
        public List<string>? ValidPowderTypes { get; set; }
        [Include]
        public float KgsRequired { get; set; }
        public bool IsActionAvailable(GearItem item)
        {
            var m_PowderItem = item.m_PowderItem;
            if (m_PowderItem == null
             || m_PowderItem.m_Weight.ToQuantity(1) < KgsRequired
             ||(ValidPowderTypes != null &&!ValidPowderTypes.Contains(m_PowderItem.m_Type.name)))
                return false;

            return true;
        }
    }

}
