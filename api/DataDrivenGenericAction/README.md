# Data Driven Generic Action

In the [Examples](./Examples/) folder you may find the following
- `hammercan.json` demonstrate an action that allows players to turn recycled cans into scrap metals with hammers. Just like the one provided by ThingsToDo.
- `campingtools.json` is a 1:1 replication of the EAAPI actions implemented in *CampingTools* mod by Jods. While this action is useless now because the mod itself is not working at the moment, this is a good example of how to create a Data Driven Generic Action.
- `fat_to_torch.json` is a 1:1 replication of the Make Torch action in Things To Do.
- `empty.json` is a raw and primitve action json that can be used as a starting point.

The `examples.eaapi` zip file contains the json files, try drop it into `/Mods` and see the example action start working right away.

--- ↓　LLM write English better than me ↓　---

A DataDrivenGenericAction (DDGA) is like a LEGO set for creating new actions in the game. Instead of writing complex code, you build your action by snapping together different "blocks" called **Providers**.

## Core Concept: Modular Design

Think of a `DataDrivenGenericAction` as a skeleton. On its own, it doesn't do much. To make it work, you attach **Providers** to handle specific logic.

| - | - |
| - | - |
|  what audio to play during the action? | **AudioNameProvider** |
|   custom code at specific events (Start, Success, Failure)? | **CallbackProvider** |
|   if the player can perform the action (e.g. calories, condition)? | **CanPerformProvider** |
|   how much liquid is consumed from the subject? | **ConsumingLiquidLitersProvider** |
|   how much powder is consumed from the subject? | **ConsumingPowderKgsProvider** |
|   how many units (stackable items) are consumed from the subject? | **ConsumingUnitsProvider** |
|   how long does the action take in game minutes? | **DurationMinuteProvider** |
|   what is the chance of failure? | **FailureChanceProvider** |
|   custom info (like "Required: 500 cal") in the UI? | **InfoConfigProvider** |
|   when should the action button appear in the menu? | **IsActionAvailableProvider** |
|    if the player needs to look at something specific (e.g. fire)? | **IsPointingToValidObjectProvider** |
|   if the action allowed at the current time of day? | **IsValidTimeProvider** |
|   if the action allowed during the current weather? | **IsValidWeatherProvider** |
|   if the action requires light? | **LightRequirementTypeProvider** |
|   what items are consumed as materials? | **MaterialItemProvider** |
|   what liquids are consumed as materials? | **MaterialLiquidProvider** |
|   what powders are consumed as materials? | **MaterialPowderProvider** |
|   what items are produced? | **ProductItemProvider** |
|   what liquids are produced? | **ProductLiquidProvider** |
|   what powders are produced? | **ProductPowderProvider** |
|   how long does the action take in real-time seconds (progress bar)? | **ProgressSecondProvider** |
|   when should the action be interrupted (e.g. fire goes out)? | **ShouldInterruptProvider** |
|    if the action have multiple sub-options (like "Harvest 1", "Harvest 2")? | **SubActionCountProvider** |
|   what tools can be used for this action? | **ToolOptionsProvider** |

## How to Find the Right "Block" (Provider)

You don't need to be a programmer to find what you need. Just follow these steps to browse the codebase:

1.  **Identify the Behavior**: Ask yourself, "What part of the action do I want to define?"
    *   *Example: "I want to make sure the player has a stack of 5 of the item"*

2.  **Search by Name**: Look for files that end with `Provider.cs` in the `api/DataDrivenGenericAction/ProviderImplmentations/` folder. The names are usually very descriptive.
    *   *Search for:* `*CanPerformProvider*`
    *   *You might find:* `SimpleCanPerformProvider.cs`

3.  **Check the "Menu"**: Open the `DataDrivenGenericAction.cs` file. This file lists all the "slots" where you can plug in providers.
    *   Look for properties ending in `Provider`.
    *   *Example:* `public ICanPerformProvider? CanPerformProvider { get; set; }`

4.  **Read the Instructions**: Open the provider file you found (e.g., `SimpleCanPerformProvider.cs`). Look at the properties marked with `[TinyJSON2.Include]` (or `[Include]`). These are the settings you can configure in your JSON file.
    *   *Example:* In `SimpleCanPerformProvider`, you might see `public float MinNormalizedCondition { get; set; }`. This tells you that you can set a minimum condition for the item in your JSON.

## Writing the JSON

Once you know which provider you want to use, writing the JSON is just filling in the blanks.

*   The **Key** is the property name from `DataDrivenGenericAction.cs` (e.g., `"CanPerformProvider"`).
*   The **Value** is an object containing the settings you found in the provider file (e.g., `"MinGearNormalizedCondition": 0.5`).
*   You also need to specify the **Type** of the provider so the game knows which "block" to load. This is done with the `$type` field.

### Example Workflow

1.  **Goal**: Limit action to items with > 50% condition.
2.  **Find Slot**: In `DataDrivenGenericAction.cs`, found `CanPerformProvider`.
3.  **Find Block**: Searched for `*CanPerformProvider` and found `SimpleCanPerformProvider`.
4.  **Check Settings**: Opened `SimpleCanPerformProvider.cs` and saw `MinGearNormalizedCondition`.
5.  **Write JSON**:

```json
"CanPerformProvider": {
    "$type": "ExamineActionsAPI.DataDrivenGenericAction.SimpleCanPerformProvider, ExamineActionsAPI",
    "MinGearNormalizedCondition": 0.5,
    "MaxGearNormalizedCondition": 1.0
}
```

## Summary

*   Start with a basic `DataDrivenGenericAction` JSON structure.
*   Check `DataDrivenGenericAction.cs` to see what behaviors you can customize.
*   Browse `ProviderImplmentations` to find pre-made logic blocks.
*   Open the provider file to see what knobs and dials you can turn (properties with `[Include]`).

## Notes

Most, but not all, of the entries in a DDGA json file can be omitted for simplicity. You usually can remove lines with `null` value to keep your json file short. But in case like `SimpleCanPerformProvider`, if you only set `MinStackSize` but left `MaxStackSize` size, because a stack is never 0 sized, the action can never be performed.

---

# Advanced: Extending the Framework (For Coders)

If the built-in providers don't do what you need, EAPI can be intuitively extended with additional providers. For example, for the `fat_to_torch` action, `IsPointingToBurningFireProvider`, `MinCaloriesCanPerformProvider`, `SpecificGearToolOptionsProvider` are created.

## Steps to Create a Custom Provider

1.  **Identify the Interface**: Determine which interface corresponds to the behavior you want to customize.
    *   *Example:* `ICanPerformProvider` for check conditions, `IToolOptionsProvider` for tool selection logic.
    *   Check `api/DataDrivenGenericAction/ProviderInterfaces/` for all available interfaces.

2.  **Implement the Class**: Create a new class that implements the chosen interface. This class can be part of your own mod assembly.

3.  **Expose Data to JSON**: Use the `[TinyJSON2.Include]` attribute on any public properties you want to be configurable via the JSON file.

4.  **Use in JSON**: Refer to your custom class using its fully qualified name (Namespace.ClassName, AssemblyName) in the `$type` field of your JSON.

## Example: Creating a Custom Calorie Check

```csharp
using ExamineActionsAPI;
using ExamineActionsAPI.DataDrivenGenericAction;

namespace MyCustomMod
{
    public class MinCaloriesCanPerformProvider : ICanPerformProvider
    {
        [TinyJSON2.Include]
        public float MinCalories { get; set; }

        public bool CanPerform(ExamineActionState state)
        {
            if (state.Subject?.m_FoodItem == null) return false;
            return state.Subject.m_FoodItem.m_CaloriesRemaining >= MinCalories;
        }
    }
}
```

**Corresponding JSON Usage:**

```json
"CanPerformProvider": {
    "$type": "MyCustomMod.MinCaloriesCanPerformProvider, MyCustomMod",
    "MinCalories": 500
}
```

## Best Practices

*   **Single Responsibility**: Keep providers focused on doing one thing well.
*   **No Side Effects**: `CanPerform` and `IsActionAvailable` checks should generally be read-only and not modify game state.
*   **Dependencies**: If your provider relies on other mods or specific types, ensure your mod declares those dependencies so the assembly is loaded before the JSON is parsed.
