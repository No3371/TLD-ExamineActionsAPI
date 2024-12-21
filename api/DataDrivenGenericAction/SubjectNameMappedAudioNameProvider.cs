using MelonLoader.TinyJSON;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SubjectNameMappedAudioNameProvider : IGetAudioNameProvider
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
