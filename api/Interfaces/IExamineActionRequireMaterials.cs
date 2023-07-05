namespace ExamineActionsAPI
{
	// Note: While it's safe to set the materials the same type of gear as the subject being examined
	// It won't be condisered a material candidate (only other stacks are)
    public interface IExamineActionRequireItems
	{
		/// <summary>
		/// Decides the shown ingredients and can the action be performed. Won't get removed automatically on success/failure.
		/// Can return null if no materials is required (for one of the sub actions).
		/// </summary>
		/// <value>("GEAR_XXX", n)</value>
		void GetRequiredItems(ExamineActionState state, List<(string gear_name, int units, byte chance)> items);
	}
}
