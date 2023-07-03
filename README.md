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
- Without implementing other interfaces, the action will always success and be finished

### IExamineActionRequireMaterials

- Supports up to 5 materials
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to consume
    - The chance the maaterial is consumed, ranging from 0 to 100
- Edge case: Subject and Materials are same type of gears
    - The subject item/stack is always ignored/excluded in the material check and consumation.
    - This means if the subject is stackable and it always stack, the edge case will never works because you never have 2 stack of that type of items.
    - Example: An action available to Stick and has Stick in the material list will never works because you can't have 2 stacks of stick.

### IExamineActionProduceItems

- Supports up to 5 products
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to produce
    - The chance the product is yieled, ranging from 0 to 100.

### IExamineActionFailable

- Make the action possible to fail by chance

### IExamineActionInterruptable

- Make the action possible to be interrupted due to light/conditions/afflictions...

### IExamineActionRequireTool

- Make the action requires tool to be performed.
- You can also adjust how much condition to be reduced on the tool.

### IExamineActionCustomInfo

- Display up to 2 information like how the duration/chance is shown.

## Demo

There are 4 actions availabe in the demo mod:

- Paper From Books: you can tear books into paper stacks.
    - Available on any researchable items.
- Brute Force Sharpening: you can spend hours to mildly sharpen tools.
    - Available on any sharpenable items.
- Slice Meat: You can cut a small piece from meats.
    - Available on any meat or fish.
- Prepare Acorns: You can prepare up to 3 acorns or 1 large portion, at once.
    - Available on acorns.
- (ItemPile compat) Easily pile sticks/coals/charcoals/cattails/stones without crafting.
    - Only availabe when stickpile item is found registered.