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
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents the result of issuing a query for a provider
    /// </summary>
    public class QueryProviderResult
    {
        /// <summary>
        /// Gets or sets the count of total items from a query
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the items as the result of a query
        /// </summary>
        public IEnumerable Items { get; set; }
    }
}
