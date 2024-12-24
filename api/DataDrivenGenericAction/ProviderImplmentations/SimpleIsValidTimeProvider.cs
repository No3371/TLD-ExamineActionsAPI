namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleIsValidTimeProvider : IIsValidTimeProvider
    {
        public SimpleIsValidTimeProvider() {}
        [TinyJSON2.Include]
        public int? MinDay { get; set; }
        [TinyJSON2.Include]
        public int? MaxDay { get; set;}
        /// <value>0.0 ~ 23.59</value>
        [TinyJSON2.Include]
        public float MinTime { get; set; }
        /// <value>0.0 ~ 23.59</value>
        [TinyJSON2.Include]
        public float MaxTime { get; set; }


        public bool IsValidTime(ExamineActionState state, TLDDateTimeEAPI time)
        {
            if (MinDay != null && time.Day < MinDay)
                return false;

            if (MaxDay != null && time.Day > MaxDay)
                return false;

            if (MinTime > time.NormalizedDayHours)
                return false;
            
            if (MaxTime < time.NormalizedDayHours)
                return false;

            return true;
        }
    }
}
