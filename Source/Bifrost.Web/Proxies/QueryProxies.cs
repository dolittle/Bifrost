using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Proxies
{
    public class QueryProxies
    {
        ITypeDiscoverer _typeDiscoverer;
        IEnumerable<PropertyInfo> _baseProperties = typeof(IQueryFor<>).GetProperties();

        public QueryProxies(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
        }


        public string Generate()
        {
            var first = true;
            var builder = new StringBuilder();
            var types = _typeDiscoverer.FindMultiple(typeof(IQueryFor<>));
            builder.AppendLine("Bifrost.namespace(\"queries\", {");

            foreach (var type in types)
            {
                if (!first) builder.Append(",\n");
                var name = type.Name.ToCamelCase();

                builder.AppendFormat("\t{0} : Bifrost.read.Query.extend(function() {{\n", name);
                builder.AppendFormat("\t\tthis.name = '{0}';\n", name);

                var properties = GetPropertiesForQuery(type);
                foreach (var property in properties)
                {
                    if (property.PropertyType.HasInterface(typeof(IDictionary<,>)) ||
                        property.PropertyType.HasInterface<IDictionary>())
                    {
                        builder.AppendFormat("\t\tthis.{0} = {{}};\n", property.Name.ToCamelCase());
                    }
                    else if ((property.PropertyType.HasInterface(typeof(IEnumerable<>)) ||
                              property.PropertyType.HasInterface<IEnumerable>()) && property.PropertyType != typeof(string))
                    {
                        builder.AppendFormat("\t\tthis.{0} = ko.observableArray();\n", property.Name.ToCamelCase());
                    }
                    else
                    {
                        builder.AppendFormat("\t\tthis.{0} = ko.observable();\n", property.Name.ToCamelCase());
                    }
                }

                builder.AppendFormat("\t}})");

                first = false;
            }



            builder.AppendLine("\n});");

            return builder.ToString();
        }

        IEnumerable<PropertyInfo> GetPropertiesForQuery(Type type)
        {
            var properties = type.GetProperties().Where(p => !_baseProperties.Select(pi => pi.Name).Contains(p.Name));
            return properties;
        }

    }
}
