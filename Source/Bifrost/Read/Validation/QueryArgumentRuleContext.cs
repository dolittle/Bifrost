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
    /// Represents the context that is needed for the <see cref="QueryArgument{TQ, TA}"/> to be able to evaluate
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TArgument"></typeparam>
    public class QueryArgumentRuleContext<TQuery, TArgument> : IRuleContext
    {
        /// <summary>
        /// Gets the query that is part of the context
        /// </summary>
        public TQuery Query { get; private set; }

        /// <summary>
        /// Gets the argument that is part of the context
        /// </summary>
        public TArgument Argument { get; private set; }
    }
}
