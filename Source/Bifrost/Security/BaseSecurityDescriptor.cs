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
using System.Linq;
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="ISecurityDescriptor"/>
    /// </summary>
    public class BaseSecurityDescriptor : ISecurityDescriptor
    {
        List<ISecurityAction> _actions = new List<ISecurityAction>();

        /// <summary>
        /// Initializes a new instance of <see cref="BaseSecurityDescriptor"/>
        /// </summary>
        public BaseSecurityDescriptor()
        {
            When = new SecurityDescriptorBuilder(this);
        }

#pragma warning disable 1591 // Xml Comments

        public ISecurityDescriptorBuilder When { get; private set; }

        public void AddAction(ISecurityAction securityAction)
        {
            _actions.Add(securityAction);
        }

        public IEnumerable<ISecurityAction> Actions { get { return _actions; } }
        
        public bool CanAuthorize<T>(object instanceToAuthorize) where T : ISecurityAction
        {
            return _actions.Where(a => a.GetType() == typeof(T)).Any(a => a.CanAuthorize(instanceToAuthorize));
        }

        public AuthorizeDescriptorResult Authorize(object instanceToAuthorize)
        {
            var result = new AuthorizeDescriptorResult();
            foreach (var action in Actions.Where(a => a.CanAuthorize(instanceToAuthorize)))
            {
               result.ProcessAuthorizeActionResult(action.Authorize(instanceToAuthorize));
            }
            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
