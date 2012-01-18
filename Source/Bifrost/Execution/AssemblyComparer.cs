using System.Collections.Generic;
using System.Reflection;

namespace Bifrost.Execution
{
	/// <summary>
	/// Represents a comparer for comparing assemblies, typically used in Distinct() 
	/// </summary>
	public class AssemblyComparer : IEqualityComparer<Assembly>
	{
#pragma warning disable 1591 // Xml Comments
		public bool Equals(Assembly x, Assembly y)
		{
			return x.FullName == y.FullName;
		}

		public int GetHashCode(Assembly obj)
		{
			return obj.GetHashCode();
		}
#pragma warning restore 1591 // Xml Comments
	}
}