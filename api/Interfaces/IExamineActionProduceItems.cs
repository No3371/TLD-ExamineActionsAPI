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
		/// <summary>
		/// Use this to force a GearItem to be used as the prefab. Otherwise by default EAAPI get a prefab by the item name.
		/// </summary>
		GearItem OverrideProductPrefab(ExamineActionState state, int index) => null;
		/// <summary>
		/// Implement this to do something to the actual products.
		/// </summary>
		void PostProcessProduct (ExamineActionState state, int index, GearItem product) {}
		/// <summary>
		/// Implement this to do something to the preview gear item on the panel. For example, without setting the current HP, raw meat seems stale in the preview.
		/// </summary>
		void PostProcessProductPreview (ExamineActionState state, int index, GearItem product) {}
	}
}
