// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Calculates the duration of the action in in-game minutes.
/// Duration = BaseDurationMinutes + (DurationMinutesOffsetPerSubAction * SubActionId)
/// </summary>
public class SimpleDurationMinutesProvider : IDurationMinutesProvider
{
	[TinyJSON2.Include]
	public int BaseDurationMinutes { get; set; }
	[TinyJSON2.Include]
	public int DurationMinutesOffsetPerSubAction { get; set; }
	public int GetDurationMinutes(ExamineActionState state) => BaseDurationMinutes + DurationMinutesOffsetPerSubAction * state.SubActionId;
}
