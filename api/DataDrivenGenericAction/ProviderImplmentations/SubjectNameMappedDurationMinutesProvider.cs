// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectNameMappedDurationMinutesProvider : IDurationMinutesProvider
    {
        [TinyJSON2.Include]
		public Dictionary<string, int> Map { get; set;}
        [TinyJSON2.Include]
		public int? DefaultDurationMinutes { get; set; }
        public int GetDurationMinutes(ExamineActionState state)
        {
			int mins = DefaultDurationMinutes?? 5;
			Map.TryGetValue(state.Subject.name, out mins);
			return mins;
        }
    }
}
