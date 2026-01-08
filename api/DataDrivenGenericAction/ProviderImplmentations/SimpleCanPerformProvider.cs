// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Checks if the action can be performed based on:
    /// 1. Subject name match (if ValidGearNames is provided)
    /// 2. Subject condition range (Base + Offset * SubActionId)
    /// 3. Subject stack size (Base + Offset * SubActionId)
    /// </summary>
    public class SimpleCanPerformProvider : ICanPerformProvider
	{
		public SimpleCanPerformProvider() { }

        [TinyJSON2.Include]
		public List<string>? ValidGearNames { get; set; }
        [TinyJSON2.Include]
		public float MinGearNormalizedCondition { get; set;}
        [TinyJSON2.Include]
		public float MaxGearNormalizedCondition { get; set;}
        [TinyJSON2.Include]
		public float MinOffsetPerSubAction { get; set; }
        [TinyJSON2.Include]
		public float MaxOffsetPerSubAction { get; set; }
        [TinyJSON2.Include]
		public int MinStackSize { get; set; }
        [TinyJSON2.Include]
		public int MaxStackSize { get; set; }
        [TinyJSON2.Include]
		public int MinStackSizeOffsetPerSubAction { get; set; }
        [TinyJSON2.Include]
		public int MaxStackSizeOffsetPerSubAction { get; set; }
		public bool CanPerform(ExamineActionState state)
		{
			if ((ValidGearNames?.Contains(state.Subject.name) ?? true) == false)
			{
				state.CustomWarningMessageOnBlocked = "Invalid gear";
				return false;
			}

			if (state.Subject.GetNormalizedCondition() < MinGearNormalizedCondition + (state.SubActionId * MinOffsetPerSubAction)
			 || state.Subject.GetNormalizedCondition() > MaxGearNormalizedCondition + (state.SubActionId * MaxOffsetPerSubAction))
			{
				state.CustomWarningMessageOnBlocked = "Invalid condition";
				return false;
			}
			
			if ((state.Subject.GetStackableItem()?.m_Units ?? 0) > MaxStackSize + (state.SubActionId * MaxStackSizeOffsetPerSubAction))
			{
				state.CustomWarningMessageOnBlocked = "Invalid stack size";
				return false;
			}

			return true;
		}
	}
}
