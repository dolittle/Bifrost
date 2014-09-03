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
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Extensions to help chain simple rules into complex rules
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Combines two atomic rules into a single rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <param name="merge">Expression for merging the two rules</param>
        /// <returns></returns>
        public static Specification<T> Compose<T>(this Specification<T> lhs, Specification<T> rhs, Func<Expression, Expression, Expression> merge)
        {
            return new CompositeRule<T>(lhs, rhs, merge);
        }

        /// <summary>
        /// Combines two rules in to an "And" rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
		public static Specification<T> And<T>(this Specification<T> rhs, Specification<T> lhs)
		{
			return new And<T>(rhs, lhs);
		}

        /// <summary>
        /// Combines two rules in to an "And" rul, where the second rule is negated.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
        public static Specification<T> AndNot<T>(this Specification<T> rhs, Specification<T> lhs)
        {
            return new And<T>(rhs, Is.Not(lhs));
        }

        /// <summary>
        /// Combines two rules in to an "Or" rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An Or{T} rule"></returns>
		public static Specification<T> Or<T>(this Specification<T> rhs, Specification<T> lhs)
		{
			return new Or<T>(rhs, lhs);
		}

        /// <summary>
        /// Combines two rules into an Or, where the second rule is negated.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rhs">The rule being extended</param>
        /// <param name="lhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
        public static Specification<T> OrNot<T>(this Specification<T> rhs, Specification<T> lhs)
        {
            return new Or<T>(rhs, Is.Not(lhs));
        }
    }

    /// <summary>
    /// Helps chain simple <see cref="Specification{T}"/> together
    /// </summary>
    public static class Is
    {
        /// <summary>
        /// Creates a Not rule based on the rule passed in.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rule">The rule being extended</param>
        /// <returns>A Not{T} rule"></returns>
        public static Specification<T> Not<T>(Specification<T> rule)
        {
            return new Negative<T>(rule);
        }
    }
}