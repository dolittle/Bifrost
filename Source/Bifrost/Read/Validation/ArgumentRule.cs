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
using Bifrost.Rules;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents the basis for an argument rule
    /// </summary>
    public abstract class ArgumentRule<TQuery, TArgument> : IRule
    {
#pragma warning disable 1591 // Xml Comments
        public bool IsSatistfiedBy(IRuleContext context)
        {
            throw new System.NotImplementedException();
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Evaluates from a <see cref="QueryArgumentRuleContext{TQuery, TArgument}"/> wether or not the rule is satisfied
        /// </summary>
        /// <param name="context"><see cref="QueryArgumentRuleContext{TQuery, TArgument}"/> to evalute from</param>
        /// <returns>True if satisfied, false if not</returns>
        protected abstract bool IsSatisfiedBy(QueryArgumentRuleContext<TQuery, TArgument> context);
    }
}