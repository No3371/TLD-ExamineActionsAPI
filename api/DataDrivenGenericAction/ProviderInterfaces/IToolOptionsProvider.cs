// #define VERY_VERBOSE
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IToolOptionsProvider
	{
		void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools);
	}
}
