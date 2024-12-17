using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI
{
    public interface IExamineActionHasExternalConstraints
	{
		bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject);
		Weather.IndoorState GetRequiredIndoorState (ExamineActionState state);
		bool IsValidWeather (ExamineActionState state, Weather weather);
		bool IsValidTime (ExamineActionState state, TLDDateTimeEAPI time);
	}
}
