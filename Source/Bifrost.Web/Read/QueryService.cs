using System;
using System.Collections;
using System.Collections.Generic;
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
        IEnumerable<Type> _queryTypes;

        public QueryService(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _queryTypes = _typeDiscoverer.FindMultiple(typeof(IQueryFor<>));
            
        }

        public IEnumerable Execute(QueryDescriptor descriptor)
        {
            var nameOfQuery = descriptor.NameOfQuery.ToPascalCase();
            var queryType = _queryTypes.SingleOrDefault(t => t.Name == nameOfQuery);
            if (queryType != null)
            {
                var instance = _container.Get(queryType);

				PopulateProperties (descriptor, queryType, instance);

                var queryProperty = queryType.GetProperty("Query");
                var queryable = queryProperty.GetValue(instance, null) as IQueryable;
                return queryable;
            }
            
            return new object[0];
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
