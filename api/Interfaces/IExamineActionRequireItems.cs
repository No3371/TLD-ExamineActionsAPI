namespace ExamineActionsAPI
{
    // Note: While it's safe to set the materials the same type of gear as the subject being examined
    // It won't be condisered a material candidate (only other stacks)
    public interface IExamineActionRequireItems
	{
		/// <summary>
		/// <para> Decides the required material items.</para>
		/// <para> Required items are checked before performing the action and the items will be consumed.</para>
		/// <para> Items with non-100 chance are required to perform but will not be consumed if the roll doesn't pass. </para>
		/// <para> Can add nothing to the list to make it free (ex: for one of the sub actions)</para>
		/// </summary>
		/// <param name="items">("GEAR_NNN", N, 0-100)</param>
		void GetMaterialItems(ExamineActionState state, List<MaterialOrProductItemConf> items);
	}
}
