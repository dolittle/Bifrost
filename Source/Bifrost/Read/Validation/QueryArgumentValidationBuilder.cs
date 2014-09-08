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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents a builder for building validation description for a query
    /// </summary>
    public class QueryArgumentValidationBuilder<TQuery, TArgument> where TQuery : IQuery
    {
        List<ArgumentRule<TQuery, TArgument>> _rules;

        /// <summary>
        /// Initializes the builder for building validation for a query argument
        /// </summary>
        /// <param name="argument"></param>
        public QueryArgumentValidationBuilder(Expression<Func<TQuery, TArgument>> argument)
        {
            _rules = new List<ArgumentRule<TQuery, TArgument>>();
        }

        /// <summary>
        /// Gets the rules from the builder
        /// </summary>
        public IEnumerable<ArgumentRule<TQuery, TArgument>> Rules { get { return _rules; } }

        /// <summary>
        /// Add a rule the builder
        /// </summary>
        /// <param name="rule"><see cref="ArgumentRule{TQ, TA}"/> to add</param>
        public void AddRule(ArgumentRule<TQuery, TArgument> rule)
        {

        }
    }
}
