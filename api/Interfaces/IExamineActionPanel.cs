namespace ExamineActionsAPI
{
    public interface IExamineActionPanel
	{
		void Toggle (bool toggle);
		void OnActionSelected (ExamineActionState state);
		void OnActionDeselected (ExamineActionState state);
		void OnPerformingAction (ExamineActionState state);
		void OnBlockedPerformingAction (ExamineActionState state, PerformingBlockedReased reason);
		void OnActionSucceed(ExamineActionState state);
		void OnActionFailed(ExamineActionState state);
		void OnActionCancelled(ExamineActionState state);

		void OnActionInterrupted(ExamineActionState state, bool force);
		void OnSelectingTool (ExamineActionState state);
		void OnSelectingToolChanged (ExamineActionState state);
		void OnToolSelected (ExamineActionState state); 
	}

	public enum PerformingBlockedReased
	{
		Action,
		Requirements,
		Interruption
	}
}
