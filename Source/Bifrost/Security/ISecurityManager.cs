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

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a manager for dealing with security for types and namespaces
    /// </summary>
    public interface ISecurityManager
    {
        /// <summary>
        /// Ask if an instance of a action is authorized
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ISecurityAction"/> that we with to authorize</typeparam>
        /// <param name="target">Object that is subject of security</param>
        /// <returns><see cref="AuthorizationResult"/> that contains the result</returns>
        AuthorizationResult Authorize<T>(object target) where T : ISecurityAction;
    }
}
