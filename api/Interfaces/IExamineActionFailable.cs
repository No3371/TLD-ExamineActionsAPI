namespace ExamineActionsAPI
{
    /// <summary>
    /// Actions implementing this may randomly fail based on the provided chance.
    /// </summary>
    public interface IExamineActionFailable
	{
        /// <summary>
        /// Chance to fail. Range: 0 - 100
        /// </summary>
		float CalculateFailureChance (ExamineActionState state);
        /// <summary>
        /// Get called on failure
        /// </summary>
		void OnActionFailure (ExamineActionState state);
        /// <summary>
        /// Will the gear be consumed on failure
        /// </summary>
		bool ShouldConsumeOnFailure (ExamineActionState state);
	}
}
