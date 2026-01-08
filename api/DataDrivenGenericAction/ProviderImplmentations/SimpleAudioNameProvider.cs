using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Returns an audio event name based on the current sub-action index.
    /// </summary>
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
