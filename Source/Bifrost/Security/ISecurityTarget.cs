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
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a <see cref="ISecurityTarget"/>
    /// </summary>
    public interface ISecurityTarget
    {
        /// <summary>
        /// Add a <see cref="ISecurable"/> 
        /// </summary>
        /// <param name="securable"><see cref="ISecurityActor"/> to add</param>
        void AddSecurable(ISecurable securable);

        /// <summary>
        /// Get all <see cref="ISecurable">securables</see>
        /// </summary>
        IEnumerable<ISecurable> Securables { get; }

        /// <summary>
        /// Indicates whether this target can authorize the instance of this action
        /// </summary>
        /// <param name="actionToAuthorize">An instance of the action to authorize</param>
        /// <returns>True if the <see cref="ISecurityTarget"/> can authorize this action, False otherwise</returns>
        bool CanAuthorize(object actionToAuthorize);

        /// <summary>
        /// Authorizes this <see cref="ISecurityTarget"/> for the instance of the action
        /// </summary>
        /// <param name="actionToAuthorize">Instance of an action to be authorized</param>
        /// <returns>An <see cref="AuthorizeTargetResult"/> that indicates if the action was authorized or not</returns>
        AuthorizeTargetResult Authorize(object actionToAuthorize);

        /// <summary>
        /// Gets a description of the SecurityTarget.
        /// </summary>
        string Description { get; }
    }
}
