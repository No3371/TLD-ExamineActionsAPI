// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubActionIdMappedDurationMinutesProvider : IDurationMinutesProvider
	{
        [TinyJSON2.Include]
		public Dictionary<int, int> Map { get; set; }

        public int GetDurationMinutes(ExamineActionState state)
        {
            return Map[state.SubActionId];
        }
    }
}
