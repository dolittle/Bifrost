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

using Bifrost.Security;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a manager for dealing with security for <see cref="Fetching">fetching read models</see>
    /// </summary>
    public interface IFetchingSecurityManager
    {
        /// <summary>
        /// Authorizes a <see cref="IReadModelOf{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/> - typically inferred by usage</typeparam>
        /// <param name="readModelOf"><see cref="IReadModelOf{T}"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IReadModelOf{T}"/> was authorized</returns>
        AuthorizationResult Authorize<T>(IReadModelOf<T> readModelOf) where T : IReadModel;

        /// <summary>
        /// Authorizes a <see cref="IQuery"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IQueryFor{T}"/> was authorized</returns>
        AuthorizationResult Authorize(IQuery query);

        /// <summary>
        /// Authorizes a <see cref="IQueryFor{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/> - typically inferred by usage</typeparam>
        /// <param name="queryFor"><see cref="IQueryFor{T}"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IQueryFor{T}"/> was authorized</returns>
        AuthorizationResult Authorize<T>(IQueryFor<T> queryFor) where T : IReadModel;
    }
}
