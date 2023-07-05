using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    public interface IExamineActionProducePowder
	{
		void GetProductPowder (ExamineActionState state, List<MaterialOrProductPowderConf> powders);
	}
}
