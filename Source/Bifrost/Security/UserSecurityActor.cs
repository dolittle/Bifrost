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
using System.Threading;
using Bifrost.Principal;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a concrete <see cref="SecurityActor"/> for a user
    /// </summary>
    public class UserSecurityActor : SecurityActor
    {
        /// <summary>
        /// Description of the <see cref="UserSecurityActor"/>
        /// </summary>
        public const string USER = "User";

        /// <summary>
        /// Instantiates an instance of <see cref="UserSecurityActor"/>
        /// </summary>
        public UserSecurityActor() : base(USER)
        {}

        /// <summary>
        /// Checks whether the Current user has the requested role.
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True is the user has the role, False otherwise</returns>
        public virtual bool IsInRole(string role)
        {
            return CurrentPrincipal.Get().IsInRole(role);
        }
    }
}
