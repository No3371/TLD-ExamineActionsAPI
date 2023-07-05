using Il2Cpp;

namespace ExamineActionsAPI
{
	/// <summary>
	/// Actions implementing this can yield prodcuts (like Harvest)
	/// </summary>
    public interface IExamineActionProduceItems
	{
		/// <summary>
		/// The products to yield
		/// </summary>
		/// <value>("GEAR_XXX", number, chance)</value>
		void GetProducts(ExamineActionState state, List<MaterialOrProductItemConf> products);
		GearItem OverrideProductPrefab(ExamineActionState state, int index) => null;
		/// <summary>
		/// Implement this to do something about the products
		/// </summary>
		void PostProcessProduct (ExamineActionState state, int index, GearItem product) {}
		/// <summary>
		/// <para> Override consumption of each material with this.</para>
		/// <para> If the material is not guaranteed to be consumed (<100%), this only get called when the it's rolled to be consumed.</para>
		/// <para> It's recommended to only override this for interruption and cancellation to compensate long actions.</para>
		/// </summary>
		// int OverrideProductItemYield(ExamineActionState state, int index, MaterialOrProductItemConf conf, ActionResult result) => conf.Units;
	}
}
