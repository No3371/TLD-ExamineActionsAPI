namespace ExamineActionsAPI
{
    /// <summary>
    /// Actions implementing this can be cancelled by players.
    /// </summary>
    public interface IExamineActionCancellable
	{
        /// <summary>
        /// This is not supposed to change during the action
        /// </summary>
		bool CanBeCanceled (ExamineActionState state) => true;
        /// <summary>
        /// Called on cancellation
        /// </summary>
        /// <param name="state"></param>
		void OnActionCanceled (ExamineActionState state);
        /// <summary>
        /// Will the gear still be consumed on cancellation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
		bool ConsumeOnCancel (ExamineActionState state);
	}
}
