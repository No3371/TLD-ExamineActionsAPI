using Il2Cpp;
using Il2CppTLD.Gear;

namespace ExamineActionsAPI
{
	/// <summary>
	/// Resolves shared gear prefabs (not instances) by gear name through kept addressable references,
	/// so repeated lookups reuse one handle per gear name.
	/// </summary>
	internal static class GearPrefabs
	{
		static readonly Dictionary<string, AssetReferenceGearItem> references = new();

		/// <summary>Returns the loaded prefab of the gear, or null if the name is invalid or loading fails.</summary>
		internal static GearItem? Get(string gearName)
		{
			if (string.IsNullOrEmpty(gearName)) return null;
			if (!references.TryGetValue(gearName, out var reference))
				references[gearName] = reference = new AssetReferenceGearItem(gearName);
			try { return reference.GetOrLoadTypedAsset(); }
			catch (Exception e) { ExamineActionsAPI.VeryVerboseLog($"Prefab load failed for {gearName}: {e.Message}"); return null; }
		}

		/// <summary>Releases all held handles. Prefabs returned earlier must not be used afterwards.</summary>
		internal static void ReleaseAll()
		{
			foreach (var reference in references.Values)
				try { reference.ReleaseAsset(); } catch (Exception e) { ExamineActionsAPI.VeryVerboseLog($"Prefab release failed: {e.Message}"); }
			references.Clear();
		}
	}
}
