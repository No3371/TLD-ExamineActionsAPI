// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Returns true if any of the provided IShouldInterruptProvider instances return true.
    /// Essentially performs a logical OR operation on the results of the sub-providers.
    /// </summary>
    public class LogicOrShouldInterruptProvider : IShouldInterruptProvider
    {
        [TinyJSON2.Include]
        public IShouldInterruptProvider[] Providers { get; set; }
        public bool ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            foreach (var provider in Providers)
                if (provider.ShouldInterrupt(state, ref message))
                    return true;
            return false;
        }
    }
}
