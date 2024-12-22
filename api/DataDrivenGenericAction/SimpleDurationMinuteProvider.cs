// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleDurationMinutesProvider : IDurationMinutesProvider
	{
        public SimpleDurationMinutesProvider () {}

        [MelonLoader.TinyJSON.Include]
		public int BaseDurationMinutes { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int DurationMinutesOffsetPerSubAction { get; set; }
		public int GetDurationMinutes(ExamineActionState state) => BaseDurationMinutes + DurationMinutesOffsetPerSubAction * state.SubActionId;
	}
}
