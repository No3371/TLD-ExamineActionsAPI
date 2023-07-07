# ExamineActionsAPI

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/66c2b391-f448-4243-b912-ecc1354b2f1e

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/0e4a3a36-4e96-4c0e-8d74-e28311d9ae7c

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/ef699713-a31a-40cf-a389-fe685bcf321c

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/c7b85a53-3038-477f-b014-3e07ed6da257

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

It's a very flexible framework, a lot of options can be combined to design custom actions that have totally dynamic behaviors according to what is being examined, what tools is selected... etc.

## Demo

There are 9 actions availabe in the demo mod:

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
- (Bountiful Foraging compat) Harvest Fir cones.
- (Bountiful Foraging compat) Make Fir Cones Seeds bunches.

## Usage

Start by implementing IExamineAction on your action class, once all the required properties are implemented, it's good to go. Call  `ExamineActionsAPI.Regsiter()` to regsiter the action.

Please refer to the [example mod](https://github.com/No3371/TLD-ExamineActionsAPI/blob/master/demo/ThingsToDo.cs) files under the demo folder.

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

### IExamineActionRequireLiquid
### IExamineActionProduceLiquid
### IExamineActionRequirePowder
### IExamineActionProducePowder

### IExamineActionFailable

- Make the action possible to fail by chance

### IExamineActionInterruptable

- Make the action possible to be interrupted due to light/conditions/afflictions...

### IExamineActionCancellable

- Make the action can be cancelled by players
  
### IExamineActionRequireTool

- Make the action requires tool to be performed.
- You can also adjust how much condition to be reduced on the tool.

### IExamineActionCustomInfo

- Display up to 2 information like how the duration/chance is shown.
