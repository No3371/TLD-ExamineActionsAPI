// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleFailureChanceProvider : IFailureChanceProvider
    {
        public SimpleFailureChanceProvider() {}
        [MelonLoader.TinyJSON.Include]
		public int BaseFailureChance { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int FailureChanceOffsetPerSubAction { get; set; }
        public float GetFailureChance(ExamineActionState state)
        {
            return BaseFailureChance + FailureChanceOffsetPerSubAction * state.SubActionId;
        }
    }
}
