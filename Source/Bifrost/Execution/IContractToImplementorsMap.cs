#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that handles the relationship between contracts and their implementors
    /// </summary>
    /// <remarks>
    /// A contract is considered an abstract type or an interface
    /// </remarks>
    public interface IContractToImplementorsMap
    {
        /// <summary>
        /// Feed the map with types
        /// </summary>
        /// <param name="types"><see cref="IEnumerable{Type}">Types</see> to feed with</param>
        void Feed(IEnumerable<Type> types);

        /// <summary>
        /// Gets all the types in the map
        /// </summary>
        IEnumerable<Type> All { get; }

        /// <summary>
        /// Retrieve implementors of a specific contract
        /// </summary>
        /// <typeparam name="T">Type of contract to retrieve for</typeparam>
        /// <returns><see cref="IEnumerable{T}">Types</see> implementing the contract</returns>
        IEnumerable<Type> GetImplementorsFor<T>();
        /// <summary>
        /// Retrieve implementors of a specific contract
        /// </summary>
        /// <param name="contract"><see cref="Type"/> of contract to retrieve for</param>
        /// <returns><see cref="IEnumerable{T}">Types</see> implementing the contract</returns>
        IEnumerable<Type> GetImplementorsFor(Type contract);
    }
}
