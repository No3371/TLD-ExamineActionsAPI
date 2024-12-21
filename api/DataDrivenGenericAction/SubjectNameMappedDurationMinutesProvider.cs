// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectNameMappedDurationMinutesProvider : IDurationMinutesProvider
    {
        [MelonLoader.TinyJSON.Include]
		public Dictionary<string, int> Map { get; set;}
        [MelonLoader.TinyJSON.Include]
		public int? DefaultDurationMinutes { get; set; }
        public int CalculateDurationMinutes(ExamineActionState state)
        {
			int mins = DefaultDurationMinutes?? 5;
			Map.TryGetValue(state.Subject.name, out mins);
			return mins;
        }
    }
}
