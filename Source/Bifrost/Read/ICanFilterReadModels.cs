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
using System.Collections.Generic;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a filter that can be applied to a set of <see cref="IReadModel">ReadModels</see>
    /// </summary>
    /// <remarks>
    /// Typically this is applied when getting both a single <see cref="IReadModel"/> and when executing a <see cref="IQueryFor">Query</see> for a <see cref="IReadModel"/>
    /// </remarks>
    public interface ICanFilterReadModels
    {
        /// <summary>
        /// Filters an incoming <see cref="IEnumerable{IReadModel}"/>
        /// </summary>
        /// <param name="readModels"><see cref="IEnumerable{IReadModel}">ReadModels</see> to filter</param>
        /// <returns>Filtered <see cref="IEnumerable{IReadModel}">ReadModels</see></returns>
        IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels);
    }
}
