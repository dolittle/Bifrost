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
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Negates a rule.  Rule is satisfied if the provided rule is not satisfied.
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evalued for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class Negative<T> : Specification<T>
    {
        internal Negative(Specification<T> rule)
        {
            Predicate = Expression.Lambda<Func<T, bool>>(Expression.Not(rule.Predicate.Body), rule.Predicate.Parameters);
        }
    }
}