# ExamineActionsAPI

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

It's a very flexible framework, a lot of options can be combined to design custom actions that have totally dynamic behaviors according to what is being examined, what tools is selected... etc.

## Usage

```csharp
ExamineActionAPI.Register(new YourAction());
```

Action documentation is WIP. At the moment please refer to the example mod files under the demo folder.

## Interfaces

### IExamineAction

- The core interface, classes implementing this are qualified as custom actions
- Without implementing other interfaces, the action will always success and be finished
    - Only exception is the action is forcefully interrupted in situations like wolf attacks
- Action menu name & sprite, button text and progress audio can be customized.

### IExamineActionRequireItems

- Setup what are required (and to be consumed) for the action
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to consume
    - The chance the maaterial is consumed, ranging from 0 to 100
- Edge case: Subject and Materials are same type of gears
    - The subject item/stack is always ignored/excluded in the material check and consumation.
    - For example, you are examine a stack of 99 sticks, even if the action only requires 1 sticks, it can't be performed because the stack of 99 sticks is excluded.

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

- Paper From Books: Tear books into paper stacks.
    - Available on any researchable items.
- Brute Force Sharpening: Spend hours to merely sharpen tools without whetstone.
    - Available on any sharpenable items.
- Slice Meat: Cut a small piece from meats.
    - Available on any meat or fish.
- Prepare Acorns: Prepare up to 3 acorns or 1 large portion, at once.
    - Available on acorns.
- Unload Storm Lanterns: Unload fuel from storm lanterns.
    - Available on storm lanterns.
- (ItemPile compat) Pile sticks/coals/charcoals/cattails/stones without crafting.
    - Only availabe when stickpile item is found registered.
- (ItemPile compat) unpile multiple piles of sticks/coals/charcoals/cattails/stones at once.
    - Only availabe when stickpile item is found registered.
- (Bountiful Foraging compat) Fir Cones harvesting and making bunches.