using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public interface IExamineActionRequireLiquid
	{
		void GetRequireLiquid (ExamineActionState state, List<(LiquidType, int, byte)> liquids);
	}
}
