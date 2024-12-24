using Il2Cpp;
using TinyJSON2;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
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
