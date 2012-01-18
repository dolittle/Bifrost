using System;

namespace Bifrost.Domain
{
	/// <summary>
	/// Represents a base class used for AggregatedRoots that will share events with another
	/// AggregatedRoot
	/// </summary>
	/// <typeparam name="T">Type of aggregated root that serves as the event source</typeparam>
	public class AggregatedRootOf<T> : AggregatedRoot
		where T:AggregatedRoot
	{
		/// <summary>
		/// Initializes a new instance of an <see cref="AggregatedRootOf{T}"/>
		/// </summary>
		/// <param name="id">Id of the AggregatedRoot</param>
		protected AggregatedRootOf(Guid id) : base(id) {}

#pragma warning disable 1591 // Xml Comments
		protected override Type EventSourceType { get { return typeof(T); } }
#pragma warning restore 1591 // Xml Comments
	}
}