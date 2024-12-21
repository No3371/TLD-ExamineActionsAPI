using Il2Cpp;

namespace ExamineActionsAPI
{
	/// <summary>
	/// Actions implementing this may be interrupted when conditions met.
	/// You may want to always implement this for long actions so players won't die when performing your actions.
	/// </summary>
    public interface IExamineActionInterruptable
	{
		/// <summary>
		/// Action not finished, for example: light goes out during during the process.
		/// </summary>
		void OnInterruption (ExamineActionState state);
		/// <summary>
		/// Will the subject be consumed on interruption
		/// </summary>
		bool ShouldConsumeOnInterruption (ExamineActionState state) => false;
		ActionsToBlock? GetLightRequirementType (ExamineActionState state);
		bool InterruptOnStarving { get; }
		bool InterruptOnExhausted { get; }
		bool InterruptOnFreezing { get; }
		bool InterruptOnDehydrated { get; }
		/// <summary>
		/// Will (non-risk) afflictions interrupt the action. (Non-Risk means not something like Infection Risk)
		/// </summary>
		bool InterruptOnNonRiskAffliction { get; }
		/// <summary>
		/// When the player's condition is below this value, the action will be interrupted
		/// </summary>
		/// <value>0-1</value>
		float NormalizedConditionInterruptThreshold { get; }
		/// <summary>
		/// Define custom conditions to trigger interruption. It is recommended to show HUDMessage to explain to player on returning true.
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		bool CustomShouldInterrupt (ExamineActionState state) => false;
	}
}
