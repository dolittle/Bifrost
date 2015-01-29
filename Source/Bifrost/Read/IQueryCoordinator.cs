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
namespace Bifrost.Read
{
    /// <summary>
    /// Defines a coordinator of queries
    /// </summary>
    public interface IQueryCoordinator
    {
        /// <summary>
        /// Execute a <see cref="IQuery"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> to execute</param>
        /// <param name="paging"><see cref="PagingInfo"/> applied to the query</param>
        /// <returns><see cref="QueryResult">Result</see> of the query</returns>
        QueryResult Execute(IQuery query, PagingInfo paging);
    }
}
