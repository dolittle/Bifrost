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
using Bifrost.Commands;

namespace Bifrost.Domain
{
	/// <summary>
	/// Represents a implementation of <see cref="ICommandForType{T}">ICommandForType</see>
	/// targetting <see cref="AggregatedRoot">AggregatedRoots</see>
	/// </summary>
	/// <typeparam name="T">Type of aggregated root the command is targetting</typeparam>
	public partial class AggregatedRootCommand<T> : ICommandForType<T>
		where T:AggregatedRoot
	{
#pragma warning disable 1591 // Xml Comments
		public Guid Id { get; set; }
#pragma warning restore 1591 // Xml Comments
	}
}
