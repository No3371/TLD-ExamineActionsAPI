# ExamineActionsAPI

The API provides a easy way to add new examine actions (like harvest/repair/sharpen) to the game The Long Dark.

It's a very flexible framework, a lot of options can be combined to design custom actions that have totally dynamic behaviors according to what is being examined, what tools is selected... etc.

## Usage

Documentation is WIP. At the moment please refer to the example files under the demo folder.

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

```csharp
ExamineActionAPI.Register(new YourAction());
```