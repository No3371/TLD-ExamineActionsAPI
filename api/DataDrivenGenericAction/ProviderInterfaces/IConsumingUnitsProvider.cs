namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IConsumingUnitsProvider
    {
        int GetConsumingUnits(ExamineActionState state);
    }
}
