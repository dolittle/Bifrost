/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using Bifrost.Dynamic;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Logging;
using Bifrost.Read;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Read
{
    public class QueryService
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IQueryCoordinator _queryCoordinator;
        WebConfiguration _configuration;

        ILogger _logger;

        public QueryService(
            ITypeDiscoverer typeDiscoverer,
            IContainer container,
            IQueryCoordinator queryCoordinator,
            WebConfiguration configuration,
            ILogger logger)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _queryCoordinator = queryCoordinator;
            _configuration = configuration;
            _logger = logger;
        }

        public QueryResult Execute(QueryDescriptor descriptor, PagingInfo paging)
        {
            try
            {
                _logger.Information($"Executing query : {descriptor.NameOfQuery}");
                var queryType = _typeDiscoverer.GetQueryTypeByName(descriptor.GeneratedFrom);
                var query = _container.Get(queryType) as IQuery;

                PopulateProperties(descriptor, queryType, query);

                var result = _queryCoordinator.Execute(query, paging);
                if (result.Success) AddClientTypeInformation(result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error executing query : '{descriptor.NameOfQuery}'")
                return new QueryResult { Exception = ex };
            }
        }

        void AddClientTypeInformation(QueryResult result)
        {
            var items = new List<object>();
            foreach (var item in result.Items)
            {
                var dynamicItem = item.AsExpandoObject();
                var type = item.GetType();

                if (_configuration.NamespaceMapper.CanResolveToClient(type.Namespace))
                    dynamicItem._sourceType = string.Format("{0}.{1}", _configuration.NamespaceMapper.GetClientNamespaceFrom(type.Namespace), type.Name.ToCamelCase());

                items.Add(dynamicItem);
            }
            result.Items = items;
        }

        void PopulateProperties(QueryDescriptor descriptor, Type queryType, object instance)
        {
            foreach (var key in descriptor.Parameters.Keys)
            {
                var propertyName = key.ToPascalCase();
                var property = queryType.GetTypeInfo().GetProperty(propertyName);
                if (property != null)
                {
                    var value = descriptor.Parameters[key].ToString().ParseTo(property.PropertyType);
                    property.SetValue(instance, value, null);
                }
            }
        }
    }
}
