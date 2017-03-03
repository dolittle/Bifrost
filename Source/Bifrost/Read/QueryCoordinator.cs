/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Exceptions;
using Bifrost.Execution;
using Bifrost.Read.Validation;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IQueryProvider"/>
    /// </summary>
    public class QueryCoordinator : IQueryCoordinator
    {
        const string QueryPropertyName = "Query";
        const string ExecuteMethodName = "Execute";

        Dictionary<Type, Type> _queryProviderTypesPerTargetType;
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;
        readonly IFetchingSecurityManager _fetchingSecurityManager;
        readonly IQueryValidator _validator;
        readonly IReadModelFilters _filters;
        readonly IExceptionPublisher _exceptionPublisher;

        /// <summary>
        /// Initializes a new instance of <see cref="QueryCoordinator"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering <see cref="IQueryProviderFor{T}"/> implementations</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="IQueryProviderFor{T}">query providers</see></param>
        /// <param name="fetchingSecurityManager"><see cref="IFetchingSecurityManager"/> to use for securing <see cref="IQuery">queries</see></param>
        /// <param name="validator"><see cref="IQueryValidator"/> to use for validating <see cref="IQuery">queries</see></param>
        /// <param name="filters"><see cref="IReadModelFilters">Filters</see> used to filter any of the read models coming back after a query</param>
        /// <param name="exceptionPublisher">An <see cref="IExceptionPublisher"/> to send exceptions to</param>
        public QueryCoordinator(
            ITypeDiscoverer typeDiscoverer, 
            IContainer container, 
            IFetchingSecurityManager fetchingSecurityManager,
            IQueryValidator validator,
            IReadModelFilters filters,
            IExceptionPublisher exceptionPublisher)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _fetchingSecurityManager = fetchingSecurityManager;
            _validator = validator;
            _filters = filters;
            _exceptionPublisher = exceptionPublisher;
            DiscoverQueryTypesPerTargetType();
        }


#pragma warning disable 1591 // Xml Comments
        public QueryResult Execute(IQuery query, PagingInfo paging)
        {
            ThrowIfNoQueryPropertyOnQuery(query);

            var result = QueryResult.For(query);

            try
            {
                var authorizationResult = _fetchingSecurityManager.Authorize(query);
                if (!authorizationResult.IsAuthorized)
                {
                    result.SecurityMessages = authorizationResult.BuildFailedAuthorizationMessages();
                    result.Items = new object[0];
                    return result;
                }
                result.Validation = _validator.Validate(query);
                if (!result.Validation.Success)
                {
                    result.Items = new object[0];
                    return result;
                }

                var property = GetQueryPropertyFromQuery(query);
                var actualQuery = property.GetValue(query, null);
                var provider = GetQueryProvider(query, property, actualQuery);
                var providerResult = ExecuteOnProvider(provider, actualQuery, paging);
                result.TotalItems = providerResult.TotalItems;
                var readModels = providerResult.Items as IEnumerable<IReadModel>;
                result.Items = readModels != null ? _filters.Filter(readModels) : providerResult.Items;
            }
            catch (TargetInvocationException ex)
            {
                _exceptionPublisher.Publish(ex.InnerException);
                result.Exception = ex.InnerException;
            }
            catch (Exception ex)
            {
                _exceptionPublisher.Publish(ex);
                result.Exception = ex;
            }

            return result;
        }



#pragma warning restore 1591 // Xml Comments


        void ThrowIfNoQueryPropertyOnQuery(IQuery query)
        {
            var property = GetQueryPropertyFromQuery(query);
            if (property == null)
                throw new NoQueryPropertyException(query);
        }

        void ThrowIfUnknownQueryType(Type queryProviderType, IQuery query, PropertyInfo property)
        {
            if (queryProviderType == null) throw new UnknownQueryTypeException(query, property.PropertyType);
        }


        PropertyInfo GetQueryPropertyFromQuery(IQuery query)
        {
            var property = query.GetType().GetTypeInfo().GetProperty(QueryPropertyName, BindingFlags.Public | BindingFlags.Instance);
            return property;
        }

        object GetQueryProvider(IQuery query, PropertyInfo property, object actualQuery)
        {
            Type queryProviderType = null;
            if (actualQuery != null && actualQuery.GetType() != property.PropertyType)
                queryProviderType = GetActualProviderTypeFrom(actualQuery.GetType());

            if (queryProviderType == null)
                queryProviderType = GetActualProviderTypeFrom(property.PropertyType);
            ThrowIfUnknownQueryType(queryProviderType, query, property);
            var provider = _container.Get(queryProviderType);
            return provider;
        }


        QueryProviderResult ExecuteOnProvider(object provider, object query, PagingInfo paging)
        {
            var method = provider.GetType().GetTypeInfo().GetMethod(ExecuteMethodName);
            var result = method.Invoke(provider, new[] { query, paging }) as QueryProviderResult;
            return result;
        }

        Type GetQueryTypeFrom(Type type)
        {
            var queryProviderForType = type.GetTypeInfo().GetInterface(typeof(IQueryProviderFor<>).FullName);
            var queryType = queryProviderForType.GetTypeInfo().GetGenericArguments()[0];
            return queryType;
        }

        Type GetActualProviderTypeFrom(Type type)
        {
            if (_queryProviderTypesPerTargetType.ContainsKey(type))
                return _queryProviderTypesPerTargetType[type];
            else
            {
                var interfaces = type.GetTypeInfo().GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    type = GetActualProviderTypeFrom(@interface);
                    if (type != null)
                        return type;
                }
            }
            return null;
        }

        void DiscoverQueryTypesPerTargetType()
        {
            var queryTypes = _typeDiscoverer.FindMultiple(typeof(IQueryProviderFor<>));

            _queryProviderTypesPerTargetType = queryTypes.Select(t => new
            {
                TargetType = GetQueryTypeFrom(t),
                QueryProviderType = t
            }).ToDictionary(t => t.TargetType, t => t.QueryProviderType);
        }
    }
}
