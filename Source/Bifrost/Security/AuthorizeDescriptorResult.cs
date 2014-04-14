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
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization attempt
    /// </summary>
    public class AuthorizeDescriptorResult
    {
        readonly List<AuthorizeActionResult> _authorizationFailures = new List<AuthorizeActionResult>();

        /// <summary>
        /// Gets any <see cref="AuthorizeActionResult"> results</see> that were not authorized
        /// </summary>
        public IEnumerable<AuthorizeActionResult> AuthorizationFailures
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
        /// Processes instance of an <see cref="AuthorizeActionResult"/>, adding failed authorizations to the AuthorizationFailures collection
        /// </summary>
        /// <param name="result">Result to process</param>
        public void ProcessAuthorizeActionResult(AuthorizeActionResult result)
        {
            if(!result.IsAuthorized)
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