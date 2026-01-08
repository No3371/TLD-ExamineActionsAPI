namespace ExamineActionsAPI.DataDrivenGenericAction;

/// <summary>
/// Executes a random callback from the list of callbacks.
/// </summary>
public class RandomCallbackProvider : ICallbackProvider
{
    [TinyJSON2.Include]
    public ICallbackProvider[] Providers { get; set; }
    public void Run(ExamineActionState state)
    {
        Providers[UnityEngine.Random.Range(0, Providers.Length)].Run(state);
    }
}