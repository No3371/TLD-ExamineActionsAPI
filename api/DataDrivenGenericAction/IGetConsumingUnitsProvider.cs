namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IGetConsumingUnitsProvider
    {
        int GetConsumingUnits(ExamineActionState state);
    }
}
