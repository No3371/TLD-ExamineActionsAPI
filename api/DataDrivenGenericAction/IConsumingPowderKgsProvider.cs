namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IConsumingPowderKgsProvider
    {
        float GetConsumingPowderKgs(ExamineActionState state);
    }
}
