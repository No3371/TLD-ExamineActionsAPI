using MelonLoader.TinyJSON;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleGetAudioNameProvider : IGetAudioNameProvider
    {
        [Include]
        public string[] AudioNameBySubAction { get; set; }
        public string? GetAudioName(ExamineActionState state)
        {
            return AudioNameBySubAction[state.SubActionId];
        }
    }
}
