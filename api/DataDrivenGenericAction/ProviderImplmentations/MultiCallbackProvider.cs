namespace ExamineActionsAPI.DataDrivenGenericAction;

public partial class CallbackSetSubjectCondition
{
    public class MultiCallbackProvider : ICallbackProvider
    {
        [TinyJSON2.Include]
        public ICallbackProvider[] Providers { get; set; }
        public void Run(ExamineActionState state)
        {
            foreach (var provider in Providers)
                provider.Run(state);
        }
    }
}