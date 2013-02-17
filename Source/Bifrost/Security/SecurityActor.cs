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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityActor"/>
    /// </summary>
    public class SecurityActor : ISecurityActor
    {
        List<ISecurityRule> _rules = new List<ISecurityRule>();

        /// <summary>
        /// Instantiates an instance of <see cref="SecurityActor"/>
        /// </summary>
        /// <param name="description">String that describes this actor type</param>
        public SecurityActor(string description)
        {
            Description = description ?? string.Empty;
        }

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

        public string Description { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
