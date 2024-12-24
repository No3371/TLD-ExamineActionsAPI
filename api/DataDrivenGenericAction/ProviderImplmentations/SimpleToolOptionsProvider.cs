// #define VERY_VERBOSE
using Il2Cpp;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleToolOptionsProvider : IToolOptionsProvider
	{
        public SimpleToolOptionsProvider() {}
        [TinyJSON2.Include]
		public ToolsItem.ToolType? ToolTypeFilter { get; set; }
        [TinyJSON2.Include]
		public ToolsItem.CuttingToolType? CuttingToolTypeFilter { get; set; }
        [TinyJSON2.Exclude]
		Il2CppSystem.Collections.Generic.List<GearItem> cache = new();
		public void GetToolOptions(ExamineActionState state, Il2CppSystem.Collections.Generic.List<GameObject> tools)
		{
			cache.Clear();
			var inv = GameManager.GetInventoryComponent();
			foreach (var gi in inv.m_Items)
			{
				if (gi.m_GearItem?.gameObject == null)
				    continue;
				if (ToolTypeFilter != null && ToolTypeFilter != gi.m_GearItem?.m_ToolsItem?.m_ToolType)
					continue;
				if (CuttingToolTypeFilter != null && CuttingToolTypeFilter != gi.m_GearItem?.m_ToolsItem?.m_CuttingToolType)
					continue;
				tools.Add(gi.m_GearItem.gameObject);
			}
		}
	}
}
