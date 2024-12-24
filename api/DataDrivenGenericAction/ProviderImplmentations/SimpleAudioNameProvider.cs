using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleAudioNameProvider : IAudioNameProvider
    {
        [Include]
        public string[] AudioNameBySubAction { get; set; }
        public string? GetAudioName(ExamineActionState state)
        {
            return AudioNameBySubAction[state.SubActionId];
        }
    }
}
