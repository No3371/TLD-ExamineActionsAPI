namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IShouldInterruptProvider
    {
        bool ShouldInterrupt(ExamineActionState state, ref string? message);
    }
}
