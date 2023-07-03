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
		/// <para>Is the action available for this item?</para>
		/// <para>Returning true means the Actions button will be shown in inventory UI and the action can be performed in the Examine menu.</para>
		/// </summary>
		bool IsActionAvailable (GearItem item);
		/// <summary>
		/// <para>If this return false, the action will be shown in red and can not be performed.</para>
		/// <para>Materials and tools check is covered by the API, use this if you want to apply additional conditions.</para>
		/// </summary>
		bool CanPerform (ExamineActionState state);
		void OnPerform (ExamineActionState state);
		/// <summary>
		/// How many ingame minutes is required to finish this action?
		/// </summary>
		int CalculateDurationMinutes (ExamineActionState state);
		/// <summary>
		/// How many realtime seconds minutes is required to finish this action?
		/// </summary>
		float CalculateProgressSeconds (ExamineActionState state);
		/// <summary>
		/// Called when the action is finshed and succeed.
		/// </summary>
		void OnSuccess (ExamineActionState state);
		/// <summary>
		/// What text to show on the menu button in the Examine menu.
		/// </summary>
		string MenuItemLocalizationKey { get; }
		/// <summary>
		/// What sprite to show on the menu button in the Examine menu.
		/// </summary>
		string MenuItemSpriteName { get;}
		/// <summary>
		/// What text to show on the button that will start the action, in the Examine menu.
		/// </summary>
		LocalizedString ActionButtonLocalizedString { get; }
		/// <summary>
		/// If the action should use a custom panel, provide the panel instance here.
		/// </summary>
		IExamineActionPanel? CustomPanel { get; }
		/// <summary>
		/// <para>Called when the player selects the menu button in the Examine menu</para>
		/// <para>Suggestion: Don't do anything to the player's items, do UI things only</para>
		/// </summary>
		void OnActionSelected (ExamineActionState state);
		/// <summary>
		/// <para>Called when the player deselects (selecting others) the menu button in the Examine menu</para>
		/// <para>Suggestion: Don't do anything to the player's items, do UI things only</para>
		/// </summary>
		void OnActionDeselected (ExamineActionState state);
		/// <summary>
		/// Will the gear be consumed on success?
		/// </summary>
		bool ConsumeOnSuccess (ExamineActionState state);
		/// <summary>
		/// If the action should consume the subject more than 1 unit, override it with this.
		/// </summary>
		int OverrideConsumingUnits (ExamineActionState state) => 1;
		int GetSubActionCounts (ExamineActionState state) => 1;
	}
}
