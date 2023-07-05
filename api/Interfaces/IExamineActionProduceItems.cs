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
		void GetProducts(ExamineActionState state, List<(string gear_name, int units, byte chance)> products);
		GearItem OverrideProductPrefabs(ExamineActionState state, int index) => null;
		/// <summary>
		/// Implement this to do something about the products
		/// </summary>
		/// <param name="index">The index in the product list</param>
		void PostProcessProduct (ExamineActionState state, int index, GearItem product) {}
	}
}
