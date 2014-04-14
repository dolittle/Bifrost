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

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines the basic functionality for finding and getting aggregated roots
    /// </summary>
    public interface IAggregateRootRepository
    {
        /// <summary>
        /// Get an aggregated root by id
        /// </summary>
        /// <returns>An instance of the aggregated root</returns>
        /// <exception cref="MissingAggregateRootException">Thrown if aggregated root does not exist</exception>
        object Get(Guid id);
    }

	/// <summary>
	/// Defines the basic functionality for finding and getting aggregated roots
	/// </summary>
	/// <typeparam name="T">Type of aggregated root</typeparam>
	public interface IAggregateRootRepository<T> : IAggregateRootRepository
		where T : AggregateRoot
	{
		/// <summary>
		/// Get an aggregated root by id
		/// </summary>
		/// <param name="id">Id of aggregated root to get</param>
		/// <returns>An instance of the aggregated root</returns>
		/// <exception cref="MissingAggregateRootException">Thrown if aggregated root does not exist</exception>
		new T Get(Guid id);
	}
}
