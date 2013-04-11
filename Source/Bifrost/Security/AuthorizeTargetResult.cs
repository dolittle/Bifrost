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
    /// Represents the result of an authorization of a <see cref="ISecurityTarget"/>
    /// </summary>
    public class AuthorizeTargetResult
    {
        readonly List<AuthorizeSecurableResult> _authorizationFailures = new List<AuthorizeSecurableResult>(); 

        /// <summary>
        /// Instantiates an instance of <see cref="AuthorizeTargetResult"/> for the specificed <see cref="ISecurityTarget"/>
        /// </summary>
        /// <param name="target"><see cref="ISecurityTarget"/> that this <see cref="AuthorizeTargetResult"/> pertains to.</param>
        public AuthorizeTargetResult(ISecurityTarget target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the <see cref="ISecurityTarget"/> that this <see cref="AuthorizeTargetResult"/> pertains to.
        /// </summary>
        public ISecurityTarget Target { get; private set; }

        /// <summary>
        /// Gets the <see cref="AuthorizeSecurableResult"/> for each failed <see cref="ISecurable"/> on the <see cref="ISecurityTarget"/>
        /// </summary>
        public IEnumerable<AuthorizeSecurableResult> AuthorizationFailures
        {
            get { return _authorizationFailures.AsEnumerable(); }
        }
        
        /// <summary>
        /// Indicates if the action instance has been authorized by the <see cref="ISecurityTarget"/>
        /// </summary>
        public virtual bool IsAuthorized
        {
            get { return !AuthorizationFailures.Any(); }
        }

        /// <summary>
        /// Processes an <see cref="AuthorizeSecurableResult"/>, adding it to the collection of AuthorizationFailures if appropriate
        /// </summary>
        /// <param name="result">An <see cref="AuthorizeSecurableResult"/> for a <see cref="ISecurable"/></param>
        public void ProcessAuthorizeSecurableResult(AuthorizeSecurableResult result)
        {
            if(!result.IsAuthorized)
                _authorizationFailures.Add(result);
        }

        /// <summary>
        /// Builds a collection of strings that show Target/Securable for each broken or erroring rule in<see cref="AuthorizeTargetResult"/>
        /// </summary>
        /// <returns>A collection of strings</returns>
        public virtual IEnumerable<string> BuildFailedAuthorizationMessages()
        {
            return from result in AuthorizationFailures from message in result.BuildFailedAuthorizationMessages() select Target.Description + "/" + message;
        }
    }
}