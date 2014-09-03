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
using System.Linq;
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Composes two rules into a single rule that can be evaluated atomically.
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class CompositeRule<T> : Specification<T>
	{
		internal CompositeRule(Specification<T> lhs, Specification<T> rhs, Func<Expression, Expression, Expression> merge)
		{
            var map = lhs.Predicate.Parameters.Select((f, i) => new { f, s = rhs.Predicate.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
			var secondBody = ParameterRebinder.ReplaceParameters(map, rhs.Predicate.Body);
			Predicate = Expression.Lambda<Func<T, bool>>(merge(lhs.Predicate.Body, secondBody), lhs.Predicate.Parameters);
		}
	}
}