using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
	// Note: Subject being exmained is not condisered a material candidate (only other stacks) even if it has PowderItem
    public interface IExamineActionRequirePowder
	{
		void GetMaterialPowder (ExamineActionState state, List<MaterialOrProductPowderConf> powders);
	}
}