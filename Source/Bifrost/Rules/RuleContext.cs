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
using Bifrost.Extensions;

namespace Bifrost.Rules
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuleContext"/>
    /// </summary>
    public class RuleContext : IRuleContext
    {
        List<RuleFailed> _callbacks = new List<RuleFailed>();

#pragma warning disable 1591 // Xml Comments
        public void OnFailed(RuleFailed callback)
        {
            _callbacks.Add(callback);
        }

        public void Fail(IRule rule, object instance, BrokenRuleReason reason)
        {
            _callbacks.ForEach(c => c(rule, instance, reason));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
