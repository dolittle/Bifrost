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
namespace Bifrost.Read
{
    /// <summary>
    /// Represents clauses that can be added to a query
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Gets or sets the size of the pages
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the current page number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets wether or not Paging is enabled
        /// </summary>
        public bool Enabled
        {
            get { return Size > 0; }
        }
    }
}
