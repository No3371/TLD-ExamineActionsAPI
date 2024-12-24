namespace ExamineActionsAPI.DataDrivenGenericAction;

public struct MaterialOrProductDef
{
    public MaterialOrProductDef() {}
    public MaterialOrProductDef(string name, float size, float chance)
    {
        Name = name;
        Size = size;
        Chance = chance;
    }
    [TinyJSON2.Include]
    public string Name { get; set; }


    [TinyJSON2.Include]
    public float Size { get; set; }
    [TinyJSON2.Include]
    public float Chance { get; set; }
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
