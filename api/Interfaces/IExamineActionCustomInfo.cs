namespace ExamineActionsAPI
{
	/// <summary>
	/// Shows custom info blocks like the duration & chance.
	/// </summary>
    public interface IExamineActionCustomInfo
	{
		InfoItemConfig? GetInfo1(ExamineActionState state);
		InfoItemConfig? GetInfo2(ExamineActionState state);
	}
}
