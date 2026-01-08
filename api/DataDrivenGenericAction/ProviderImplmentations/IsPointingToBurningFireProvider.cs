// #define VERY_VERBOSE
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Validates that the player is pointing at a burning fire.
    /// </summary>
    public class IsPointingToBurningFireProvider : IIsPointingToValidObjectProvider
    {
        public bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject)
        {
            if (pointedObject == null) return false;
            
            Fire fire = pointedObject.GetComponent<Fire>();
            if (fire == null) fire = pointedObject.GetComponentInChildren<Fire>();
            if (fire == null) fire = pointedObject.GetComponentInParent<Fire>();

            var burning = fire != null && fire.IsBurning();
            if (!burning)
            {
                state.CustomWarningMessageOnBlocked = "Requires a burning fire";
            }
            return burning;
        }
    }
}
