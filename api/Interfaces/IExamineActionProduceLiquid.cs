using Il2Cpp;

namespace ExamineActionsAPI
{
    /// <summary>
    /// Not implemented. Not working.
    /// </summary>
    public interface IExamineActionProduceLiquid
	{
		void GetProductLiquid (ExamineActionState state, List<(GearLiquidTypeEnum, float, byte)> liquids);
	}
}
