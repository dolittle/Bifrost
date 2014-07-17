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
using System.Collections.Generic;
using Bifrost.Dynamic;
using Bifrost.Execution;
using Bifrost.Extensions;
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

        public QueryService(ITypeDiscoverer typeDiscoverer, IContainer container, IQueryCoordinator queryCoordinator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _queryCoordinator = queryCoordinator;
            _configuration = configuration;
        }

        public QueryResult Execute(QueryDescriptor descriptor, PagingInfo paging)
        {
            var queryType = _typeDiscoverer.GetQueryTypeByName(descriptor.GeneratedFrom);
            var query = _container.Get(queryType) as IQuery;

			PopulateProperties (descriptor, queryType, query);

            var result = _queryCoordinator.Execute(query, paging);
            if( result.Success ) AddClientTypeInformation(result);
            return result;
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

		void PopulateProperties (QueryDescriptor descriptor, Type queryType, object instance)
		{
			foreach (var key in descriptor.Parameters.Keys) {
				var propertyName = key.ToPascalCase ();
				var property = queryType.GetProperty (propertyName);
				if (property != null) {
                    var value = descriptor.Parameters[key].ToString().ParseTo(property.PropertyType);
					property.SetValue (instance, value, null);
				}
			}
		}
    }
}
