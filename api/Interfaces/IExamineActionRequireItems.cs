namespace ExamineActionsAPI
{
    // Note: While it's safe to set the materials the same type of gear as the subject being examined
    // It won't be condisered a material candidate (only other stacks)
    public interface IExamineActionRequireItems
	{
		/// <summary>
		/// <para> Decides the shown ingredients and can the action be performed. Won't get removed automatically on success/failure. </para>
		/// <para> Can return null if no materials is required (for one of the sub actions). </para>
		/// </summary>
		/// <param name="items">("GEAR_NNN", N, 0-100)</param>
		void GetRequiredItems(ExamineActionState state, List<MaterialOrProductItemConf> items);
		
		/// <summary>
		/// <para> Override consumption of each material with this.</para>
		/// <para> If the material is not guaranteed to be consumed (<100%), this only get called when the it's rolled to be consumed.</para>
		/// <para> It's recommended to only override this for interruption and cancellation to compensate long actions.</para>
		/// </summary>
		// int OverrideMaterialItemConsumption(ExamineActionState state, int index, MaterialOrProductItemConf material, ActionResult result) => material.Units;
	}
}
