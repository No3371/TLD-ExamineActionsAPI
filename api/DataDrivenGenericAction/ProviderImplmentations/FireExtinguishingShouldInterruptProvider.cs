// #define VERY_VERBOSE
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class FireExtinguishingShouldInterruptProvider : IShouldInterruptProvider
    {
        public bool ShouldInterrupt(ExamineActionState state, ref string? message)
        {
            GameObject obj = GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(2.5f);
            if (obj == null) return false;
            
            Fire fire = obj.GetComponent<Fire>();
            if (fire == null) fire = obj.GetComponentInChildren<Fire>();
            if (fire == null) fire = obj.GetComponentInParent<Fire>();

            bool interrupt = fire == null || !fire.IsBurning();
            if (interrupt)
            {
                message = Localization.Get("Fire went out...");
            }
            return interrupt;
        }
    }
}
