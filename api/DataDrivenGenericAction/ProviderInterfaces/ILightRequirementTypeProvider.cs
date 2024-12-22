using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface ILightRequirementTypeProvider
    {
        ActionsToBlock? GetLightRequirementType(ExamineActionState state);
    }
}
