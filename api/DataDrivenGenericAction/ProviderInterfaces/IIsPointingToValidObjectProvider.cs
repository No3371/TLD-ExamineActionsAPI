using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Validates if the player is looking at a valid object required for the action (e.g., a fire, a workbench).
    /// </summary>
    public interface IIsPointingToValidObjectProvider
    {
        bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject);
    }
}
