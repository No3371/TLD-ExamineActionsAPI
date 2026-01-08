// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Returns true if all of the provided IShouldInterruptProvider instances return true.
    /// Essentially performs a logical AND operation on the results of the sub-providers.
    /// </summary>
    public class LogicAndShouldInterruptProvider : IShouldInterruptProvider
    {
        [TinyJSON2.Include]
        public IShouldInterruptProvider[] Providers { get; set; }
        public bool ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            foreach (var provider in Providers)
                if (!provider.ShouldInterrupt(state, ref message))
                    return false;

            return true;
        }
    }
}
