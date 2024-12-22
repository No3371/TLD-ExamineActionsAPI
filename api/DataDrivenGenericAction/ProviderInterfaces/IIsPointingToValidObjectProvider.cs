using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IIsPointingToValidObjectProvider
    {
        bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject);
    }
}
