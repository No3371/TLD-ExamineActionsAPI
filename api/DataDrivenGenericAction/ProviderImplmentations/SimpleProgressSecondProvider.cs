// #define VERY_VERBOSE
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleProgressSecondProvider : IProgressSecondProvider
	{
        public SimpleProgressSecondProvider() {}
        [TinyJSON2.Include]
		public float BaseProgressSeconds { get; set; }
        [TinyJSON2.Include]
		public float ProgressSecondsOffsetPerSubAction { get; set; }
        [TinyJSON2.Include]
		public float? MaxProgressSeconds { get; set; }
		public float GetProgressSeconds(ExamineActionState state)
		{
			return Mathf.Clamp(BaseProgressSeconds + ProgressSecondsOffsetPerSubAction * state.SubActionId, 0.5f, MaxProgressSeconds?? 600);
		}
	}
}
