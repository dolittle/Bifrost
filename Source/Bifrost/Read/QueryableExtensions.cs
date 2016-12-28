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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bifrost.Read
{
    /// <summary>
    /// Provides a set of methods for working with <see cref="IQueryable"/>
    /// </summary>
    public static class QueryableExtensions
    {
        static MethodInfo _countMethod;
        static MethodInfo _skipMethod;
        static MethodInfo _takeMethod;

        static QueryableExtensions()
        {
            var queryableMethods = typeof(System.Linq.Queryable).GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Static);

            _countMethod = queryableMethods.Where(m => m.Name == "Count" && m.GetParameters().Length == 1).First();
            _skipMethod = queryableMethods.Where(m => m.Name == "Skip").First();
            _takeMethod = queryableMethods.Where(m => m.Name == "Take").First();
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <param name="queryable">The <see cref="IQueryable"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        public static int Count(this IQueryable queryable)
        {
            var genericMethod = _countMethod.MakeGenericMethod(new Type[] { queryable.ElementType });
            var countExpression = Expression.Call(null, genericMethod, queryable.Expression);
            var count = queryable.Provider.Execute<int>(countExpression);
            return count;
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <param name="queryable">An <see cref="IQueryable"/> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>An <see cref="IQueryable"/> that contains elements that occur after the specified index in the input sequence.</returns>
        public static IQueryable Skip(this IQueryable queryable, int count)
        {
            var genericMethod = _skipMethod.MakeGenericMethod(new Type[] { queryable.ElementType });
            queryable = genericMethod.Invoke(null, new object[] { queryable, count }) as IQueryable;
            return queryable;
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <param name="queryable">The <see cref="IQueryable"/> to return elements.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IQueryable"/> that contains the specified number of elements from the start of source.</returns>
        public static IQueryable Take(this IQueryable queryable, int count)
        {
            var genericMethod = _takeMethod.MakeGenericMethod(new Type[] { queryable.ElementType });
            queryable = genericMethod.Invoke(null, new object[] { queryable, count }) as IQueryable;
            return queryable;
        }

        /// <summary>
        /// Returns true if you need to treat the Take() extension as a end index, rather than count
        /// </summary>
        /// <param name="queryable">The <see cref="IQueryable"/> to check for.</param>
        /// <returns>True if one needs to use it as end index, false if not</returns>
        public static bool IsTakeEndIndex(this IQueryable queryable)
        {
            var providerType = queryable.Provider.GetType();
            return providerType.Name == "INhQueryProvider" || providerType.Namespace.StartsWith("NHibernate");
        }
    }
}
