#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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
    /// Represents a base for any <see cref="ISecurityAction"/>
    /// </summary>
    public class SecurityAction : ISecurityAction
    {
        readonly List<ISecurityTarget> _targets = new List<ISecurityTarget>();

#pragma warning disable 1591 // Xml Comments
        public void AddTarget(ISecurityTarget securityTarget)
        {
            _targets.Add(securityTarget);
        }

        public bool CanAuthorize(object actionToAuthorize)
        {
            return _targets.Any(s => s.CanAuthorize(actionToAuthorize));
        }

        public AuthorizeActionResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeActionResult(this);
            foreach (var target in Targets)
            {
                result.AddAuthorizeTargetResult(target.Authorize(this));
            }
            return result;
        }

        public IEnumerable<ISecurityTarget> Targets { get { return _targets.AsEnumerable(); } }
#pragma warning restore 1591 // Xml Comments
    }
}
