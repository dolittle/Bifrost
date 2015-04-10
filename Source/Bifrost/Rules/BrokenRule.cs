#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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

namespace Bifrost.Rules
{
    /// <summary>
    /// Represents a broken rule
    /// </summary>
    public class BrokenRule
    {
        List<BrokenRuleReason> _reasons = new List<BrokenRuleReason>();

        /// <summary>
        /// Initializes a new instance of <see cref="BrokenRule"/>
        /// </summary>
        /// <param name="rule"><see cref="IRule"/> that is broken</param>
        /// <param name="instance">Instance related to the broken rule when evaluated</param>
        /// <param name="context"><see cref="IRuleContext"/> rule was running in</param>
        public BrokenRule(IRule rule, object instance, IRuleContext context)
        {
            Rule = rule;
            Instance = instance;
            Context = context;
        }

        /// <summary>
        /// Gets the type of rule that is broken
        /// </summary>
        public IRule Rule { get; private set; }

        /// <summary>
        /// Gets the instance used for evaluating the <see cref="IRule"/>
        /// </summary>
        public object Instance { get; private set; }

        /// <summary>
        /// Gets the context in which the rule was broken
        /// </summary>
        public IRuleContext Context { get; private set; }

        /// <summary>
        /// Gets the <see cref="BrokenRuleReason">reasons</see> why the rule is broken
        /// </summary>
        public IEnumerable<BrokenRuleReason> Reasons { get { return _reasons; } }

        /// <summary>
        /// Add a reason for the <see cref="IRule"/> being broken
        /// </summary>
        /// <param name="reason"><see cref="BrokenRuleReason">Reason</see></param>
        public void AddReason(BrokenRuleReason reason)
        {
            _reasons.Add(reason);
        }
    }
}
