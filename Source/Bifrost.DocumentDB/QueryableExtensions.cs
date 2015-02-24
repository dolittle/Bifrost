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

namespace Bifrost.DocumentDB
{
    /// <summary>
    /// Extends queryable with DocumentDB specific extensions
    /// </summary>
    public static class QueryableExtensions
    {
        static MethodInfo _whereMethod;

        static QueryableExtensions()
        {
            var queryableMethods = typeof(System.Linq.Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);

            _whereMethod = queryableMethods.Where(m => m.Name == "Where").First();
        }

        /// <summary>
        /// Specify a DocumentType (<see cref="IHaveDocumentType"/>)
        /// </summary>
        /// <typeparam name="TSource">Source of type for queryable</typeparam>
        /// <param name="queryable">Queryable</param>
        /// <param name="documentType">DocumentType to specify</param>
        /// <returns><see cref="IQueryable"/> that we can build queries on</returns>
        public static IQueryable<TSource> DocumentType<TSource>(this IQueryable<TSource> queryable, string documentType)
        {
            Expression<Func<TSource, bool>> expression = (TSource source) => ((IHaveDocumentType)source)._DOCUMENT_TYPE == documentType;

            var genericMethod = _whereMethod.MakeGenericMethod(new Type[] { queryable.ElementType });

            queryable = queryable.Provider.CreateQuery<TSource>(
                Expression.Call(
                    null,
                    genericMethod
                    ,
                new Expression[] { 
                    queryable.Expression,
                    Expression.Quote(expression)

                })
            );
            return queryable;
        }
    }
}
