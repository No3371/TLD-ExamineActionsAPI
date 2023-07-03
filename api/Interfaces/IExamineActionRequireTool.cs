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
		/// <para>Find appropriate gears as tools (usually from inventroy) and add them to the list.</para>
		/// <para>You can add null (bare hands) as an option too.</para>
		/// </summary>
		void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools);
		/// <summary>
		/// Scale the degration on use to the used tool
		/// </summary>
		/// <param name="state"></param>
		float CalculateDegradingScale(ExamineActionState state) => 1;
	}
}
