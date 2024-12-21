namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IAudioNameProvider
    {
        string? GetAudioName (ExamineActionState state);
    }
}
