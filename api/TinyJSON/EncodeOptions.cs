using System;

namespace TinyJSON2
{
	[Flags]
	public enum EncodeOptions
	{
		None = 0,
		PrettyPrint = 1,
		NoTypeHints = 2,
		IncludePublicProperties = 4,
		EnforceHierarchyOrder = 8,
	}
}
