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

using System;
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityActor"/>
    /// </summary>
    public class SecurityActor : ISecurityActor
    {
        List<ISecurityRule> _rules = new List<ISecurityRule>();

#pragma warning disable 1591 // Xml Comments
        public void AddRule(ISecurityRule rule)
        {
            _rules.Add(rule);
        }

        public IEnumerable<ISecurityRule> Rules { get { return _rules; } }

        public AuthorizeActorResult IsAuthorized(object actionToAuthorize)
        {
            var result = new AuthorizeActorResult(this);
            foreach (var rule in _rules)
            {
                try
                {
                    if(!rule.IsAuthorized(actionToAuthorize))
                        result.AddBrokenRule(rule);
                }
                catch (Exception ex)
                {
                    result.AddErrorRule(rule,ex);
                }
            }
            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
