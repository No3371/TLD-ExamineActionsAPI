namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IIsValidTimeProvider
    {
        bool IsValidTime(ExamineActionState state, TLDDateTimeEAPI time);
    }
}
