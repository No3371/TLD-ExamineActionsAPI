// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Returns the opposite of the provided IShouldInterruptProvider instance.
    /// Essentially performs a logical NOT operation on the result of the sub-provider.
    /// </summary>
    public class LogicNotShouldInterruptProvider : IShouldInterruptProvider
    {
        [TinyJSON2.Include]
        public IShouldInterruptProvider Provider { get; set; }
        public bool ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            return !Provider.ShouldInterrupt(state, ref message);
        }
    }
}
