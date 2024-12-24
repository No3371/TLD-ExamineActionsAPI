namespace ExamineActionsAPI.DataDrivenGenericAction;

public struct MaterialOrProductSizedBySubActionDef 
{
    public MaterialOrProductSizedBySubActionDef() {}
    public MaterialOrProductSizedBySubActionDef(MaterialOrProductDef def, float sizeExtraMultiplierPerSubAction, float sizeOffsetPerSubAction)
    {
        Def = def;
        SizeExtraMultiplierPerSubAction = sizeExtraMultiplierPerSubAction;
        SizeOffsetPerSubAction = sizeOffsetPerSubAction;
    }
    [TinyJSON2.Include]
    public MaterialOrProductDef Def { get; set; }

    [TinyJSON2.Include]
    public float SizeExtraMultiplierPerSubAction { get; set; } = 0;
    [TinyJSON2.Include]
    public float SizeOffsetPerSubAction { get; set; } = 0;

    public MaterialOrProductLiquidConf ToLiquidConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductLiquidConf
        {
            Type = PowderAndLiquidTypesLocator.LoadLiquid(Def.Name),
            Liters = Def.Size,
            Chance = Def.Chance
        };
        
        if (state.SubActionId != 0)
        {
            conf.Liters *= 1 + (state.SubActionId * SizeExtraMultiplierPerSubAction);
            conf.Liters += (state.SubActionId * SizeOffsetPerSubAction);
        }

        return conf;
    }

    public MaterialOrProductItemConf ToItemConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductItemConf
        {
            GearName = Def.Name,
            Units = (int) Def.Size,
            Chance = Def.Chance
        };
        
        if (state.SubActionId != 0)
        {
            conf.Units = (int) (conf.Units * (1f + state.SubActionId * SizeExtraMultiplierPerSubAction));
            conf.Units += (int) (state.SubActionId * SizeOffsetPerSubAction);
        }

        return conf;
    }

    public MaterialOrProductPowderConf ToPowderConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductPowderConf
        {
            Type = PowderAndLiquidTypesLocator.LoadPowder(Def.Name),
            Kgs = Def.Size,
            Chance = Def.Chance
        };

        if (state.SubActionId != 0)
        {
            conf.Kgs *= 1 + (state.SubActionId * SizeExtraMultiplierPerSubAction);
            conf.Kgs += (state.SubActionId * SizeOffsetPerSubAction);
        }

        return conf;
    }
}
