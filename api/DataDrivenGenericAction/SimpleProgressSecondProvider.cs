// #define VERY_VERBOSE
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProgressSecondProvider : IProgressSecondProvider
	{
        public SimpleProgressSecondProvider() {}
        [MelonLoader.TinyJSON.Include]
		public float BaseProgressSeconds { get; set; }
        [MelonLoader.TinyJSON.Include]
		public float ProgressSecondsOffsetPerSubAction { get; set; }
        [MelonLoader.TinyJSON.Include]
		public float? MaxProgressSeconds { get; set; }
		public float CalculateProgressSeconds(ExamineActionState state)
		{
			return Mathf.Clamp(BaseProgressSeconds + ProgressSecondsOffsetPerSubAction * state.SubActionId, 0.5f, MaxProgressSeconds?? 600);
		}
	}
}
