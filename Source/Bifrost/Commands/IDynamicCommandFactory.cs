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
using System.Linq.Expressions;
using Bifrost.Domain;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a factory for creating <see cref="DynamicCommand">DynamicCommands</see>
	/// </summary>
    public interface IDynamicCommandFactory
    {
		/// <summary>
		/// Create a command from an expression describing a method for the dynamic command
		/// </summary>
		/// <typeparam name="T"><see cref="AggregatedRoot"/> type to create for </typeparam>
		/// <param name="aggregatedRootId">Id of the <see cref="AggregatedRoot"/></param>
		/// <param name="method"><see cref="Expression"/> expressing the method call</param>
		/// <returns>A <see cref="DynamicCommand"/> holding information about the command</returns>
        DynamicCommand Create<T>(Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot;
    }
}