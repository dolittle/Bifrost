#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using Bifrost.Events;

namespace Bifrost.Domain
{
	/// <summary>
	/// Represents the base class used for aggregated roots in your domain
	/// </summary>
	public class AggregateRoot : EventSource, IAggregateRoot
	{
		/// <summary>
		/// Initializes a new instance of an <see cref="AggregateRoot">AggregatedRoot</see>
		/// </summary>
		/// <param name="id">Id of the AggregatedRoot</param>
	    protected AggregateRoot(Guid id) : base(id)
	    {}
	}
}
