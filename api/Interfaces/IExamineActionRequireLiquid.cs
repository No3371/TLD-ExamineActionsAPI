using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
	/// <summary>
	/// Not implemented. Not working.
	/// </summary>
    public interface IExamineActionRequireLiquid
	{
		void GetRequireLiquid (ExamineActionState state, List<(LiquidType, float, byte)> liquids);
	}
}
