namespace ExamineActionsAPI
{
	/// <summary>
	/// Shows custom info blocks like the duration & chance.
	/// </summary>
    public interface IExamineActionDisplayInfo
	{
		void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs);
	}
}
