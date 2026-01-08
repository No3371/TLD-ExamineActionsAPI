using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    /// <summary>
    /// Checks if the player is pointing at a valid object.
    /// Validates against object names and various component types (Workbench, Bed, etc.).
    /// </summary>
    public class SimpleIsPointingToValidObjectProvider : IIsPointingToValidObjectProvider
    {
        public SimpleIsPointingToValidObjectProvider() {}

        [TinyJSON2.Include]
		public List<string>? ValidObjectNames { get; set; }
        [TinyJSON2.Include]
        public bool? WorkbenchFilter { get; set; }
        [TinyJSON2.Include]
        public bool? AmmoWorkbenchFilter { get; set; }
        [TinyJSON2.Include]
        public bool? BedFilter { get; set; }
        [TinyJSON2.Include]
        public bool? WoodStoveFilter { get; set; }
        [TinyJSON2.Include]
        public bool? CookingSlotFilter { get; set; }
        [TinyJSON2.Include]
        public bool? HarvestableFilter { get; set; }
        [TinyJSON2.Include]
        public bool? ForgeFilter { get; set; }
        public bool IsPointingToValidObject(ExamineActionState state, GameObject pointedObject)
        {
            if (ValidObjectNames != null && !ValidObjectNames.Contains(pointedObject.name))
                return false;

            if (WorkbenchFilter != null && WorkbenchFilter.Value != (pointedObject.GetComponent<WorkBench>() != null))
                return false;

            if (AmmoWorkbenchFilter != null && AmmoWorkbenchFilter.Value != (pointedObject.GetComponent<AmmoWorkBench>() != null))
                return false;

            if (BedFilter != null && BedFilter.Value != (pointedObject.GetComponent<Bed>() != null))
               return false;

            if (WoodStoveFilter != null && WoodStoveFilter.Value != (pointedObject.GetComponent<WoodStove>() != null))
                return false;
            
            if (CookingSlotFilter != null && CookingSlotFilter.Value != (pointedObject.GetComponent<CookingSlot>() != null))
                return false;

            if (HarvestableFilter != null && HarvestableFilter.Value != (pointedObject.GetComponent<Harvestable>() != null))
                return false;

            if (ForgeFilter != null && ForgeFilter.Value != (pointedObject.GetComponent<Forge>() != null))
                return false;

            return true;
        }
    }
}
