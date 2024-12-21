// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubActionIdMappedDurationMinutesProvider : IDurationMinutesProvider
	{
        [MelonLoader.TinyJSON.Include]
		public Dictionary<int, int> Map { get; set; }

        public int CalculateDurationMinutes(ExamineActionState state)
        {
            return Map[state.SubActionId];
        }
    }
}
