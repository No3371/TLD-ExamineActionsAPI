// #define VERY_VERBOSE
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Defines the list of valid tools that can be used to perform this action.
    /// </summary>
    public interface IToolOptionsProvider
	{
		void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools);
	}
}
