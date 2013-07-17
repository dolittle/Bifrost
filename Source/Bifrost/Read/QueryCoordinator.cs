#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.Execution;

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
        IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="QueryCoordinator"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering <see cref="IQueryProviderFor{T}"/> implementations</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="IQueryProviderFor{T}">query providers</see></param>
        public QueryCoordinator(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;
            var queryTypes = typeDiscoverer.FindMultiple(typeof(IQueryProviderFor<>));

            _queryProviderTypesPerTargetType = queryTypes.Select(t => new { 
                TargetType = GetQueryTypeFrom(t), 
                QueryProviderType = t }).ToDictionary(t => t.TargetType, t => t.QueryProviderType);
        }

#pragma warning disable 1591 // Xml Comments
        public QueryResult Execute(IQuery query, PagingInfo paging)
        {
            ThrowIfNoQueryPropertyOnQuery(query);

            var result = new QueryResult();
            try
            {
                var property = GetQueryPropertyFromQuery(query);
                var queryProviderType = GetActualProviderTypeFrom(property.PropertyType);
                ThrowIfUnknownQueryType(queryProviderType, query, property);
                var provider = _container.Get(queryProviderType);
                var actualQuery = property.GetValue(query, null);
                var providerResult = ExecuteOnProvider(provider, actualQuery, paging);
                result.TotalItems = providerResult.TotalItems;
                result.Items = providerResult.Items;
            }
            catch (TargetInvocationException ex)
            {
                result.Exception = ex.InnerException;
            }
            catch (Exception ex)
            {
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
            var property = query.GetType().GetProperty(QueryPropertyName, BindingFlags.Public | BindingFlags.Instance);
            return property;
        }

        QueryProviderResult ExecuteOnProvider(object provider, object query, PagingInfo clauses)
        {
            var method = provider.GetType().GetMethod(ExecuteMethodName);
            var result = method.Invoke(provider, new[] { query, clauses }) as QueryProviderResult;
            return result;
        }

        Type GetQueryTypeFrom(Type type)
        {
            var queryProviderForType = type.GetInterface(typeof(IQueryProviderFor<>).FullName);
            var queryType = queryProviderForType.GetGenericArguments()[0];
            return queryType;
        }

        Type GetActualProviderTypeFrom(Type type)
        {
            if (_queryProviderTypesPerTargetType.ContainsKey(type))
                return _queryProviderTypesPerTargetType[type];
            else
            {
                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    type = GetActualProviderTypeFrom(@interface);
                    if (type != null)
                        return type;
                }
            }
            return null;
        }

    }
}
