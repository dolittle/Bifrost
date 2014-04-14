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
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines an action that is subject to security
    /// </summary>
    public interface ISecurityAction
    {
        /// <summary>
        /// Add a <see cref="ISecurityTarget"/> 
        /// </summary>
        /// <param name="securityTarget"><see cref="ISecurityTarget"/> to add</param>
        void AddTarget(ISecurityTarget securityTarget);

        /// <summary>
        /// Get all <see cref="ISecurityTargets">security targets</see>
        /// </summary>
        IEnumerable<ISecurityTarget> Targets { get; }

        /// <summary>
        /// Indicates whether this action can authorize the instance of the action
        /// </summary>
        /// <param name="actionToAuthorize">An instance of the action to authorize</param>
        /// <returns>True if the <see cref="ISecurityAction"/> can authorize this action, False otherwise</returns>
        bool CanAuthorize(object actionToAuthorize);

        /// <summary>
        /// Authorizes this <see cref="ISecurityAction"/> for the instance of the action
        /// </summary>
        /// <param name="actionToAuthorize">Instance of an action to be authorized</param>
        /// <returns>An <see cref="AuthorizeActionResult"/> that indicates if the action was authorized or not</returns>
        AuthorizeActionResult Authorize(object actionToAuthorize);

        /// <summary>
        /// Returns a string description of this <see cref="ISecurityAction"/>
        /// </summary>
        string ActionType { get; }
    }
}
