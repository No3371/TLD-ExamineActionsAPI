using Il2Cpp;
using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the light requirement for the action based on sub-action ID.
    /// </summary>
    public class SimpleLightRequirementTypeProvider : ILightRequirementTypeProvider
    {
        [Include]
        public ActionsToBlock?[] LightRequirementTypeBySubActionId { get; set; }
        public ActionsToBlock? GetLightRequirementType(ExamineActionState state)
        {
            return LightRequirementTypeBySubActionId[state.SubActionId];
        }
    }
}
