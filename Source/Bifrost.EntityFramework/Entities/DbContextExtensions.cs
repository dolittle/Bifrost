/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Extensions for working with a <see cref="DbContext"/>
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Get the key field for a specific type from a <see cref="IObjectContextAdapter"/>
        /// </summary>
        /// <typeparam name="T">Type to get key field from</typeparam>
        /// <param name="context"><see cref="DbContext"/> that holds the metadata</param>
        /// <returns>Name of key field</returns>
        public static string GetKeyField<T>(this DbContext context)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            var key = objectContext
                .MetadataWorkspace
                .GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace)
                .BaseEntitySets
                .First(meta => meta.ElementType.Name == typeof(T).Name)
                .ElementType
                .KeyMembers
                .Select(k => k.Name)
                .FirstOrDefault();

            return key;
        }

        /// <summary>
        /// Get an <see cref="IOrderedQueryable{T}"/> from a <see cref="IQueryable{T}"/> based on the <see cref="DbContext"/>
        /// </summary>
        /// <typeparam name="T">Type of entity to get for</typeparam>
        /// <param name="context"><see cref="DbContext"/> that holds the metadata</param>
        /// <param name="queryable"><see cref="IQueryable{T}"/> to get ordered version for</param>
        /// <returns>Converted <see cref="IOrderedQueryable{T}"/></returns>
        public static IOrderedQueryable<T> Ordered<T>(this DbContext context, IQueryable<T> queryable)
        {
            var key = context.GetKeyField<T>();
            return queryable.OrderBy(key);
        }


        /// <summary>
        /// Order by a specific field by its name
        /// </summary>
        /// <typeparam name="T">Type that owns the field</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> that should be ordered</param>
        /// <param name="orderBy">Field to order by</param>
        /// <returns>The <see cref="IOrderedQueryable{T}"/></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderBy)
        {
            return source.GetOrderByQuery(orderBy, "OrderBy");
        }

        /// <summary>
        /// Order by a specific field by its name, descending
        /// </summary>
        /// <typeparam name="T">Type that owns the field</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> that should be ordered</param>
        /// <param name="orderBy">Field to order by</param>
        /// <returns>The <see cref="IOrderedQueryable{T}"/></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string orderBy)
        {
            return source.GetOrderByQuery(orderBy, "OrderByDescending");
        }

        private static IOrderedQueryable<T> GetOrderByQuery<T>(this IQueryable<T> source, string orderBy, string methodName)
        {
            var sourceType = typeof(T);
            var property = sourceType.GetProperty(orderBy);
            var parameterExpression = Expression.Parameter(sourceType, "x");
            var getPropertyExpression = Expression.MakeMemberAccess(parameterExpression, property);
            var orderByExpression = Expression.Lambda(getPropertyExpression, parameterExpression);
            var resultExpression = Expression.Call(typeof(Queryable), methodName,
                                                   new[] { sourceType, property.PropertyType }, source.Expression,
                                                   orderByExpression);

            return source.Provider.CreateQuery<T>(resultExpression) as IOrderedQueryable<T>;
        }
    }
}
