using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
    /// <summary>
    /// Note: The subject is not counted as materials, so if you are examining a gunpowder container and requiring gunpowder, powder in it does not count.
    /// However, this only applys to requirement checks. Actual deduction of powder still may take powder from the suject.
    /// </summary>
    public interface IExamineActionRequirePowder
	{
		void GetRequiredPowder (ExamineActionState state, List<(PowderType, float, byte)> powders);
	}
}
