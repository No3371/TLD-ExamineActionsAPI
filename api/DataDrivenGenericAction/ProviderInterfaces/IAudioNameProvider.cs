namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Provides the audio event name (Wwise) to be played during the action.
    /// </summary>
    public interface IAudioNameProvider
    {
        string? GetAudioName (ExamineActionState state);
    }
}
