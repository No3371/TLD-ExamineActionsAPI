using Il2Cpp;

namespace ExamineActionsAPI
{

    public interface IExamineAction
	{
		/// <summary>
		/// Development Id, has no effect in game.
		/// </summary>
		/// <value></value>
		string Id { get; }
		/// <summary>
		/// Is the action available for this item? 
		/// </summary>
		bool IsActionAvailable (GearItem item);
		/// <summary>
		/// If this return false, the action will not be show in red and can not be performed.
		/// Materials and tools check is covered by the API, use this if you want to apply additional conditions.
		/// </summary>
		bool CanPerform (ExamineActionState state);
		void OnPerform (ExamineActionState state);
		int CalculateDurationMinutes (ExamineActionState state);
		float CalculateProgressSeconds (ExamineActionState state);
		void OnSuccess (ExamineActionState state);
		string MenuItemLocalizationKey { get; }
		string MenuItemSpriteName { get;}
		LocalizedString ActionButtonLocalizedString { get; }
		/// <summary>
		/// If the action should use a custom panel, provide the panel instance here
		/// </summary>
		/// <value></value>
		IExamineActionPanel? CustomPanel { get; }
		void OnActionSelected (ExamineActionState state);
		void OnActionDeselected (ExamineActionState state);
		/// <summary>
		/// Will the gear be consumed on success?
		/// </summary>
		bool ConsumeOnSuccess (ExamineActionState state);
		int GetSubActionCounts (ExamineActionState state) => 1;
	}
}
