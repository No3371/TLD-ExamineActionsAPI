// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleFailureChanceProvider : IFailureChanceProvider
    {
        public SimpleFailureChanceProvider() {}
        [TinyJSON2.Include]
		public int BaseFailureChance { get; set; }
        [TinyJSON2.Include]
		public int FailureChanceOffsetPerSubAction { get; set; }
        public float GetFailureChance(ExamineActionState state)
        {
            return BaseFailureChance + FailureChanceOffsetPerSubAction * state.SubActionId;
        }
    }
}
