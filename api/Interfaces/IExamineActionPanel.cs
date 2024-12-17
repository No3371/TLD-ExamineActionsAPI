namespace ExamineActionsAPI
{
    public interface IExamineActionPanel
	{
		/// <summary>
		/// If a panel is an extension, it will run alongside DefaultPanel (all the methods are called after DefaultPanel)
		/// </summary>
		bool IsExtension { get; }
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
		void SetBottomWarning (string message);
	}

	public enum PerformingBlockedReased
	{
		Action,
		MaterialRequirement,
		ToolRequirement,
		WeatherConstraint,
		PointedObjectConstraint,
		TimeConstraint,
		IndoorStateConstraint,
		Interruption
	}
}
