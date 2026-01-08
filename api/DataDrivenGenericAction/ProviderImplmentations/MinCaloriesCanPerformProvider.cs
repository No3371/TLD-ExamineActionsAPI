// #define VERY_VERBOSE
namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class MinCaloriesCanPerformProvider : ICanPerformProvider
    {
        [TinyJSON2.Include]
        public float MinCalories { get; set; }
        
        [TinyJSON2.Include]
        public string BlockedMessage { get; set; } = "Not enough calories";

        public bool CanPerform(ExamineActionState state)
        {
            if (state.Subject == null || state.Subject.m_FoodItem == null) return false;
            
            bool result = state.Subject.m_FoodItem.m_CaloriesRemaining >= MinCalories;
            if (!result)
            {
                state.CustomWarningMessageOnBlocked = BlockedMessage;
            }
            return result;
        }
    }
}
