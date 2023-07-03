using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI
{
	/// <summary>
	/// Actions implementing this will requires a specified tool to perform
	/// </summary>
    public interface IExamineActionRequireTool
	{
		/// <summary>
		/// Find appropriate gears as tools (usually from inventroy) and add then to the list.
		/// </summary>
		void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools);
		/// <summary>
		/// Scale the degration on use to the used tool
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		float CalculateDegradingScale(ExamineActionState state) => 1;
	}
}
