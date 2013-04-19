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
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Contains the result of an attempt to authorize an action.
    /// </summary>
    public class AuthorizationResult
    {
        readonly List<AuthorizeDescriptorResult> _authorizationFailures = new List<AuthorizeDescriptorResult>();

        /// <summary>
        /// Gets any <see cref="AuthorizeDescriptorResult"> results</see> that were not authorized
        /// </summary>
        public IEnumerable<AuthorizeDescriptorResult> AuthorizationFailures
        {
            get { return _authorizationFailures.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the result of the Authorization attempt for this action and <see cref="ISecurityDescriptor"/>
        /// </summary>
        public virtual bool IsAuthorized
        {
            get { return !_authorizationFailures.Any(); }
        }

        /// <summary>
        /// Processes instance of an <see cref="AuthorizeDescriptorResult"/>, adding failed authorizations to the AuthorizationFailures collection
        /// </summary>
        /// <param name="result">Result to process</param>
        public void ProcessAuthorizeDescriptorResult(AuthorizeDescriptorResult result)
        {
            if (!result.IsAuthorized)
                _authorizationFailures.Add(result);
        }

        /// <summary>
        /// Gets all the broken <see cref="ISecurityRule">rules</see> for this authorization attempt
        /// </summary>
        /// <returns>A string describing each broken rule or an empty enumerable if there are none</returns>
        public virtual IEnumerable<string> BuildFailedAuthorizationMessages()
        {
            var messages = new List<string>();
            foreach (var result in AuthorizationFailures)
            {
                messages.AddRange(result.BuildFailedAuthorizationMessages());
            }
            return messages;
        }
    }
}