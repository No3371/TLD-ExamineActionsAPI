# ExamineActionsAPI

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

It's a very flexible framework, a lot of options can be combined to design custom actions that have totally dynamic behaviors according to what is being examined, what tools is selected... etc.

## Usage

```csharp
ExamineActionAPI.Register(new YourAction());
```

Action documentation is WIP. At the moment please refer to the example files under the demo folder.

## Interfaces

### IExamineAction

- The core interface, classes implementing this are qualified as custom actions

### IExamineActionRequireMaterials

- Supports up to 5 materials
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to consume
    - The chance the maaterial is consumed, ranging from 0 to 100

### IExamineActionProduceItems

- Supports up to 5 products
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to produce
    - The chance the product is yieled, ranging from 0 to 100.

## Demo

There are 4 actions availabe in the demo mod:

- Paper From Books: you can tear books into paper stacks.
    - Avaialbe on any researchable items.
- Brute Force Sharpening: you can spend hours to mildly sharpen tools.
    - Avaialbe on any shaprenable.
- Slice Meat: You can cut a small piece from meats.
    - Avaialbe on any meat or fish.
- Perpare Acorns: You can perpare up to 3 acorns or 1 large portion, at once.
    - Avaialbe on acorns.