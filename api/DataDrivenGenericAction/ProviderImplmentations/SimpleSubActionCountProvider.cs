using MelonLoader.TinyJSON;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleSubActionCountProvider : ISubActionCountProvider
    {
        [Include]
        public int SubActionCount { get; set; } = 1;
        public int GetSubActionCount(ExamineActionState state)
        => SubActionCount;
    }
}
