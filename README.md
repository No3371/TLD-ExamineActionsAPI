# ExamineActionsAPI

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

## Usage

This is a low layer API, it is recommended to "Create the action" instead of "Create the action for some item". For example, to create a "Salting Meats" action, it's always a good idea to create a action that 

```csharp
class MinimalAction : IExamineAction
{
    public string Id => nameof(MinimalAction);
    public string MenuItemLocalizationKey => "Minimal";
    public string MenuItemSpriteName => null;
    public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "Minimal" };
    public IExamineActionPanel? CustomPanel => null;
    public int CalculateDurationMinutes(ExamineActionState state) => 10;
    public float CalculateProgressSeconds(ExamineActionState state) => 5;
    public bool CanPerform(ExamineActionState state) => true;
    public bool IsActionAvailable(GearItem item) => true;
    public void OnActionDeselected(ExamineActionState state) {}
    public void OnActionSelected(ExamineActionState state) {}
    public void OnPerform(ExamineActionState state) {}
    public void OnSuccess(ExamineActionState state) {}
}

ExamineActionAPI.Register(new MinimalAction());
```