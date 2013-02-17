#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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