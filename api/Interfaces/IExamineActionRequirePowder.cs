using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
	// Note: Subject being exmained is not condisered a material candidate (only other stacks) even if it has PowderItem
    public interface IExamineActionRequirePowder
	{
		void GetRequiredPowder (ExamineActionState state, List<MaterialOrProductPowderConf> powders);
		
		/// <summary>
		/// <para> Override this to control consumption on interruption if this is an IExamineActionInterruptable. </para>
		/// <para> If the material is not guaranteed to be consumed (<100%), this only get called when the roll is passed.   </para>
		/// </summary>
		// float? OverrideMaterialPowderConsumptionOnInterruption(ExamineActionState state, int index, (PowderType type, float kgs, byte chance) material) => null;
	}
}