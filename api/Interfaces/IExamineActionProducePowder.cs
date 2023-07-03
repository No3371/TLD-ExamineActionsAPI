using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    /// <summary>
    /// Not implemented. Not working.
    /// </summary>
    public interface IExamineActionProducePowder
	{
		void GetProductPowder (ExamineActionState state, List<(PowderType, float, byte)> powders);
	}
}
