using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleIsPointingToValidObjectProvider : IIsPointingToValidObjectProvider
    {
        public SimpleIsPointingToValidObjectProvider() {}
        [MelonLoader.TinyJSON.Include]
		public List<string>? ValidObjectNames { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? WorkbenchFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? AmmoWorkbenchFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? BedFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? WoodStoveFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? CookingSlotFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
        public bool? HarvestableFilter { get; set; }
        [MelonLoader.TinyJSON.Include]
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
