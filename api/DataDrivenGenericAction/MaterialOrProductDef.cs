namespace ExamineActionsAPI.DataDrivenGenericAction;

public struct MaterialOrProductDef
{
    public MaterialOrProductDef(string name, float size, byte chance)
    {
        Name = name;
        Size = size;
        Chance = chance;
    }
    [MelonLoader.TinyJSON.Include]
    public string Name { get; set; }


    [MelonLoader.TinyJSON.Include]
    public float Size { get; set; }
    [MelonLoader.TinyJSON.Include]
    public byte Chance { get; set; }
    public MaterialOrProductLiquidConf ToLiquidConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductLiquidConf
        {
            Type = PowderAndLiquidTypesLocator.LoadLiquid(Name),
            Liters = Size,
            Chance = Chance
        };

        return conf;
    }

    public MaterialOrProductItemConf ToItemConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductItemConf
        {
            GearName = Name,
            Units = (int)Size,
            Chance = Chance
        };

        return conf;
    }

    public MaterialOrProductPowderConf ToPowderConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductPowderConf
        {
            Type = PowderAndLiquidTypesLocator.LoadPowder(Name),
            Kgs = Size,
            Chance = Chance
        };

        return conf;
    }
}
