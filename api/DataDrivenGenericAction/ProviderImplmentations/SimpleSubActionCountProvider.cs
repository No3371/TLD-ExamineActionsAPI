using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Returns a fixed total number of sub-actions available. Default is 1, which means only the default sub-action is available.
    /// </summary>
    public class SimpleSubActionCountProvider : ISubActionCountProvider
    {
        [Include]
        public int SubActionCount { get; set; } = 1;
        public int GetSubActionCount(ExamineActionState state)
        => SubActionCount;
    }
}
