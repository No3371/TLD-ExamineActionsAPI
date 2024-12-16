using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
	// Note: Subject being exmained is not condisered a material candidate (only other stacks) even if it has LiquidItem
    public interface IExamineActionRequireLiquid
	{
		void GetRequiredLiquid (ExamineActionState state, List<MaterialOrProductLiquidConf> liquids);
	}
}
