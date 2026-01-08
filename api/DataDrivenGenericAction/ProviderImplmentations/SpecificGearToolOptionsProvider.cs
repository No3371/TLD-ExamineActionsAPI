// #define VERY_VERBOSE
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SpecificGearToolOptionsProvider : IToolOptionsProvider
    {
        [TinyJSON2.Include]
        public List<string> ValidGearNames { get; set; } = new();

        public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
        {
            var inv = GameManager.GetInventoryComponent();
            foreach (var gi in inv.m_Items)
            {
                if (gi != null && gi.m_GearItem != null && ValidGearNames.Contains(gi.m_GearItem.name))
                {
                    tools.Add(gi.m_GearItem.gameObject);
                }
            }
        }
    }
}
