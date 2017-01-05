/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
