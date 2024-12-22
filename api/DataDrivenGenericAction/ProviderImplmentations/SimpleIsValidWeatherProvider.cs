using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleIsValidWeatherProvider : IIsValidWeatherProvider
    {
        public SimpleIsValidWeatherProvider() {}
        [MelonLoader.TinyJSON.Include]
        public float MinTemperature { get; set; }
        [MelonLoader.TinyJSON.Include]
        public float MaxTemperature { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsSnowing { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsLightSnow { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsHeavySnow { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsClear { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsDenseFog { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsFoggy { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsElectrostaticFog { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? IsBlizzard { get; set; }

        public bool IsValidWeather(ExamineActionState state, Weather weather)
        {
            if (weather.GetCurrentTemperature() < MinTemperature || weather.GetCurrentTemperature() > MaxTemperature)
                return false;

            if (IsSnowing != null && weather.IsSnowing() != IsSnowing.Value)
                return false;

            if (IsLightSnow != null && weather.IsLightSnow() != IsLightSnow.Value)
                return false;

            if (IsHeavySnow != null && !weather.IsHeavySnow() != IsHeavySnow.Value)
                return false;

            if (IsClear != null && weather.IsClear() != IsClear.Value)
                return false;

            if (IsDenseFog != null && weather.IsDenseFog() != IsDenseFog.Value)
                return false;

            if (IsFoggy != null && weather.IsFoggy() != IsFoggy.Value)
                return false;

            if (IsElectrostaticFog != null && weather.IsElectrostaticFog() != IsElectrostaticFog.Value)
                return false;

            if (IsBlizzard != null && weather.IsBlizzard() != IsBlizzard.Value)
                return false;

            return true;
        }
    }
}
