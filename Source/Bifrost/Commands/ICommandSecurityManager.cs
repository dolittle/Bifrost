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
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a manager for dealing with security for <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandSecurityManager
    {
        /// <summary>
        /// Authorizes a <see cref="ICommand"/> 
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to ask for</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="ICommand"/> was authorized</returns>
        AuthorizationResult Authorize(ICommand command);
    }
}
