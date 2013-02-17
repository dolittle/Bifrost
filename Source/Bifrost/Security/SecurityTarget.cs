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
    /// Represents a base class for any <see cref="ISecurityTarget">security targets</see>
    /// </summary>
    public class SecurityTarget : ISecurityTarget
    {
        readonly List<ISecurable> _securables = new List<ISecurable>();

        /// <summary>
        /// Instantiats an instance of <see cref="SecurityTarget"/>
        /// </summary>
        /// <param name="description">A description for this <see cref="SecurityTarget"/></param>
        public SecurityTarget(string description)
        {
            Description = description ?? string.Empty;
        }

#pragma warning disable 1591
        public void AddSecurable(ISecurable securityObject)
        {
            _securables.Add(securityObject);
        }

        public IEnumerable<ISecurable> Securables { get { return _securables; } }

        public bool CanAuthorize(object actionToAuthorize)
        {
            return _securables.Any(s => s.CanAuthorize(actionToAuthorize));
        }

        public AuthorizeTargetResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeTargetResult(this);
            foreach (var securable in Securables)
            {
                result.ProcessAuthorizeSecurableResult(securable.Authorize(actionToAuthorize));
            }
            return result;
        }

        public string Description { get; private set; }
#pragma warning restore 1591
    }
}
