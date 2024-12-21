namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IGetAudioNameProvider
    {
        string? GetAudioName (ExamineActionState state);
    }
}
