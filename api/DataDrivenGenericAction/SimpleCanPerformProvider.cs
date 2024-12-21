// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleCanPerformProvider : ICanPerformProvider
	{
		public SimpleCanPerformProvider() { }
        [MelonLoader.TinyJSON.Include]
		public List<string>? ValidGearNames { get; set; }
        [MelonLoader.TinyJSON.Include]
		public float MinGearNormalizedCondition { get; set;}
        [MelonLoader.TinyJSON.Include]
		public float MaxGearNormalizedCondition { get; set;}
        [MelonLoader.TinyJSON.Include]
		public float MinOffsetPerSubAction { get; set; }
        [MelonLoader.TinyJSON.Include]
		public float MaxOffsetPerSubAction { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int MinStackSize { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int MaxStackSize { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int MinStackSizeOffsetPerSubAction { get; set; }
        [MelonLoader.TinyJSON.Include]
		public int MaxStackSizeOffsetPerSubAction { get; set; }
		public bool CanPerform(ExamineActionState state)
		{
			return ValidGearNames?.Contains(state.Subject.name) ?? true
			    && state.Subject.GetNormalizedCondition() >= MinGearNormalizedCondition + (state.SubActionId * MinOffsetPerSubAction)
			    && state.Subject.GetNormalizedCondition() <= MaxGearNormalizedCondition + (state.SubActionId * MaxOffsetPerSubAction)
			    && (state.Subject.GetStackableItem()?.m_Units ?? 0) >= MinStackSize + (state.SubActionId * MinStackSizeOffsetPerSubAction)
			    && (state.Subject.GetStackableItem()?.m_Units ?? 0) <= MaxStackSize + (state.SubActionId * MaxStackSizeOffsetPerSubAction);
		}
	}
}
