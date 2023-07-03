namespace ExamineActionsAPI
{
    public interface IExamineActionRequireMaterials
	{
		/// <summary>
		/// Decides the shown ingredients and can the action be performed. Won't get removed automatically on success/failure.
		/// Can return null if no materials is required (for one of the sub actions).
		/// </summary>
		/// <value>("GEAR_XXX", n)</value>
		(string, int, byte)[]? GetMaterials(ExamineActionState state);
	}
}
