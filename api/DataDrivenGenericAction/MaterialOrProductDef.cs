namespace ExamineActionsAPI.DataDrivenGenericAction;

public struct MaterialOrProductDef (string Name, float Size, byte Chance)
{
    public float SizeExtraMultiplierPerSubAction { get; set; }
    public float SizeOffsetPerSubAction { get; set; }
    public MaterialOrProductLiquidConf ToLiquidConf (ExamineActionState state)
    {
        var conf = new MaterialOrProductLiquidConf
        {
            Type = PowderAndLiquidTypesLocator.LoadLiquid(Name),
            Liters = Size,
            Chance = Chance
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
            GearName = Name,
            Units = (int)Size,
            Chance = Chance
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
            Type = PowderAndLiquidTypesLocator.LoadPowder(Name),
            Kgs = Size,
            Chance = Chance
        };

        if (state.SubActionId != 0)
        {
            conf.Kgs *= 1 + (state.SubActionId * SizeExtraMultiplierPerSubAction);
            conf.Kgs += (state.SubActionId * SizeOffsetPerSubAction);
        }

        return conf;
    }
}
