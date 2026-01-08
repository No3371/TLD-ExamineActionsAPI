using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Maps the subject item's name to a specific sub-action count.
    /// </summary>
    public class SubjectNameMappedSubActionCountProvider : ISubActionCountProvider
    {
        [Include]
        public int DefaultSubActionCount { get; set; } = 1;

        [Include]
        public Dictionary<string, int> Map { get; set; }
        public int GetSubActionCount(ExamineActionState state)
        {
            if (Map.TryGetValue(state.Subject.name, out var mapped))
                return mapped;
            else
                return DefaultSubActionCount;
        }
    }
}
