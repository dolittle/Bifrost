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
    /// Represents a base class for any <see cref="ISecurityTarget">security targets</see>
    /// </summary>
    public class SecurityTarget : ISecurityTarget
    {
        readonly List<ISecurable> _securables = new List<ISecurable>();

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
                result.AddAuthorizeSecurableResult(securable.Authorize(actionToAuthorize));
            }
            return result;
        }
#pragma warning restore 1591
    }
}
