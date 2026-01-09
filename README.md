# ExamineActionsAPI

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/66c2b391-f448-4243-b912-ecc1354b2f1e

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/0e4a3a36-4e96-4c0e-8d74-e28311d9ae7c

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/ef699713-a31a-40cf-a389-fe685bcf321c

https://github.com/No3371/TLD-ExamineActionsAPI/assets/1814971/c7b85a53-3038-477f-b014-3e07ed6da257

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

It's a very flexible framework, a lot of options can be combined to design custom actions that have totally dynamic behaviors according to what is being examined, what tools is selected... etc.

It can even be used to create a fully fledged crafting system.

## Things To Do

Things To Do is a mod which is initially made to showcase the features of ExamineActionsAPI, but eventually included actual gameplay features.

Please refer to its [readme](/demo/README.md) for more information.

## Data Driven Generic Action

The API supports creating actions with json files (no-code).

Please refer to its [readme](./api/DataDrivenGenericAction/README.md).

## Usage

Start by implementing `IExamineAction` on your action class, once all the required properties are implemented, it's good to go. Call  `ExamineActionsAPI.Regsiter()` to regsiter the action.

Please refer to action implementations in [ThingsToDo](/demo) for practical examples.

### Interfaces

#### [IExamineAction](api/Interfaces/IExamineAction.cs)

- The core interface, any class implementing this can be registered
- Without implementing other interfaces, the action will always success and be finished
    - Only exception is the action is forcefully interrupted in situations like wolf attacks
- Action menu name & sprite, button text and progress audio can be customized.
- Additional interfaces below can be implemented to introduce various mechanics to the action.

#### [IExamineActionRequireItems](api/Interfaces/IExamineActionRequireItems.cs)

- Setup what are required (and to be consumed) for the action
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to consume
    - The chance the maaterial is consumed, ranging from 0 to 100
- Edge case: Subject and Materials are same type of gears
    - The subject item/stack is always ignored/excluded in the material check and consumation.
    - For example, you are examine a stack of 99 sticks, even if the action only requires 1 sticks, it can't be performed because the stack of 99 sticks is excluded.

#### [IExamineActionProduceItems](api/Interfaces/IExamineActionProduceItems.cs)

- Supports up to 5 in total of items/liquid/powder
- The parameters of each:
    - Name of the gear ("GEAR_???")
    - How many units to produce
    - The chance the product is yieled, ranging from 0 to 100.

#### [IExamineActionRequireLiquid](api/Interfaces/IExamineActionRequireLiquid.cs)

- Supports up to 5 in total of items/liquid/powder
- The parameters of each:
    - Type object of the liquid
        - Use references in `PowderAndLiquidTypesLocator` if you want to use official types
    - How many liters to consume
    - The chance these liquid is consumed, ranging from 0 to 100

#### [IExamineActionProduceLiquid](api/Interfaces/IExamineActionProduceLiquid.cs)

- Supports up to 5 in total of items/liquid/powder
- The parameters of each:
    - Type object of the liquid
        - Use references in `PowderAndLiquidTypesLocator` if you want to use official types
    - How many liters to produce
    - The chance these liquid is produced, ranging from 0 to 100
  
#### [IExamineActionRequirePowder](api/Interfaces/IExamineActionRequirePowder.cs)

- Supports up to 5 in total of items/liquid/powder
- The parameters of each:
    - Type object of the powder
        - Use references in `PowderAndLiquidTypesLocator` if you want to use official types
    - How many kilograms to consume
    - The chance these powder is consumed, ranging from 0 to 100

#### [IExamineActionProducePowder](api/Interfaces/IExamineActionProducePowder.cs)

- Supports up to 5 in total of items/liquid/powder
- The parameters of each:
    - Type object of the powder
        - Use references in `PowderAndLiquidTypesLocator` if you want to use official types
    - How many kilograms to produce
    - The chance these powder is produced, ranging from 0 to 100

#### [IExamineActionFailable](api/Interfaces/IExamineActionFailable.cs)

- Make the action possible to fail by chance (0 ~ 100)
- You can control should the subject to be still consumed on a failure

#### [IExamineActionInterruptable](api/Interfaces/IExamineActionInterruptable.cs)

- Make the action possible to be interrupted due to light/conditions/afflictions...
- You can control should the subject to be still consumed on a interruption

#### [IExamineActionCancellable](api/Interfaces/IExamineActionCancellable.cs)

- Make the action can be cancelled by players
- You can control should the subject to be still consumed on a cancellation
  
#### [IExamineActionRequireTool](api/Interfaces/IExamineActionRequireTool.cs)

- Make the action requires tool to be performed.
- You can also scale how much condition to be reduced on the tool.

#### [IExamineActionDisplayInfo](api/Interfaces/IExamineActionDisplayInfo.cs)

- Display up to 3 information like how the duration/chance is shown.
- While the panel can display up to 4 information blocks, one is always occupied by the action time. If the action implements interfaces that automatically display related info, the blocks provided later may not be displayed.

#### [IExamineActionHasExternalConstraints](api/Interfaces/IExamineActionHasExternalConstraints.cs)

- Apply additional constraints to the action, like weather, time... or even requires the player to looking at some specific object...

#### [IExamineActionHasDependendency](api/Interfaces/IExamineActionHasDependendency.cs)

- Define dependencies required for the action to be registered, so it's easier to work with modded content.