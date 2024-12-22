# Data Driven Generic Action Examples

Here are some examples of Data Driven Generic Actions:
- `ct.json` is a 1:1 mirror of the actions implemented in *CampingTools* mod by Jods. While this action is useless now because the mod itself is not working at the moment, this is a good example of how to create a Data Driven Generic Action.
- `template.json` provides an action that is full of randomly set values and act as a reference of what should be written.
- `minimal.json` demonstrate a "blank" action. In fact, a lot of the fields can be ommited (e.g. the null ones), however it's recommended to leave null fields in the json so it's easier to know available fields.

The `example.eaapi` zip file which contains the 3 json files demonstrates the final "mod" file to put in `Mods` folder.

## Providers

DDGA features a modular design that action behaviors are defined by *providers*. Besides [built-in providers](../ProviderImplmentations/), anyone can implement their own providers and use them in action definition jsons.