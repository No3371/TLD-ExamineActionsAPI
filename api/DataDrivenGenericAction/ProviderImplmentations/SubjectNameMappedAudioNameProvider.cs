using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Maps the subject item's name to an audio event name.
    /// </summary>
    public class SubjectNameMappedAudioNameProvider : IAudioNameProvider
    {
        [Include]
        public Dictionary<string, string> Map { get; set; }
        public string? GetAudioName(ExamineActionState state)
        {
            string? audioName = null;
            Map?.TryGetValue(state.Subject.name, out audioName);
            return audioName;
        }
    }
}
