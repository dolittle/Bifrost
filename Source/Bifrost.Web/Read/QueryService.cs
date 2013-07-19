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
using System.Collections;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Read
{
    public class QueryService
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IQueryCoordinator _queryCoordinator;

        public QueryService(ITypeDiscoverer typeDiscoverer, IContainer container, IQueryCoordinator queryCoordinator)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _queryCoordinator = queryCoordinator;
        }

        public QueryResult Execute(QueryDescriptor descriptor, PagingInfo paging)
        {
            var queryType = _typeDiscoverer.GetQueryTypeByName(descriptor.GeneratedFrom);
            var query = _container.Get(queryType) as IQuery;

			PopulateProperties (descriptor, queryType, query);

            var result = _queryCoordinator.Execute(query, paging);
            return result;
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
