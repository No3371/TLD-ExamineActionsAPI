namespace ExamineActionsAPI
{
	/// <summary>
	/// Shows custom info blocks like the duration & chance.
	/// </summary>
    public interface IExamineActionCustomInfo
	{
		void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs);
	}
}
