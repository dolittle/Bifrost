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
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Base class for expressing a complex rule in code.  Utilising the Specification pattern. 
    /// </summary>
    /// <typeparam name="T">Type that the rule applies to</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx 
    /// </remarks>
    public abstract class Specification<T>
    {
        /// <summary>
        /// Compiled predicate for use against an instance
        /// </summary>
        protected Func<T, bool> evalCompiled;
        /// <summary>
        /// Predicate as an expression for use against IQueryable collection
        /// </summary>
        protected Expression<Func<T, bool>> evalExpression;

        /// <summary>
        /// Predicate rule to be evaluated
        /// </summary>
        protected internal Expression<Func<T, bool>> Predicate
        {
            get
            {
                return evalExpression;
            }
            set
            {
                evalExpression = value;
                evalCompiled = evalExpression.Compile();
            }
        }

        /// <summary>
        /// Evalutes the rule against a single instance of type T.
        /// </summary>
        /// <param name="instance">The instance to evaluation the rule against.</param>
        /// <returns>true if the rule is satisfied, false if the rule is broken</returns>
        public bool IsSatisfiedBy(T instance)
        {
            return evalCompiled(instance);
        }

        /// <summary>
        /// Evaluates the rule against each instance of an IQueryable[T]
        /// </summary>
        /// <param name="candidates">The IQueryable[T] that will have the rule evaluated against each of its instances</param>
        /// <returns>An IQueryable[T] containing only instances that satisfy the rule</returns>
        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return candidates.Where(evalExpression);
        }
    }
}
