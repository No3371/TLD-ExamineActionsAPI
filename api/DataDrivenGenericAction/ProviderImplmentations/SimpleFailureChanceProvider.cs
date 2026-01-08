// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Calculates the percentage chance of failure (0-100).
    /// Chance = BaseFailureChance + (FailureChanceOffsetPerSubAction * SubActionId)
    /// </summary>
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
