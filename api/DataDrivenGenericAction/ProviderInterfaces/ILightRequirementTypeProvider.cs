using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the light requirements for the action (e.g., requires light to see, or specific actions blocked by darkness).
    /// </summary>
    public interface ILightRequirementTypeProvider
    {
        ActionsToBlock? GetLightRequirementType(ExamineActionState state);
    }
}
