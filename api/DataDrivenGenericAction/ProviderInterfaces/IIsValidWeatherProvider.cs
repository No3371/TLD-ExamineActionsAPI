using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public interface IIsValidWeatherProvider
    {
        bool IsValidWeather(ExamineActionState state, Weather weather);
    }
}
