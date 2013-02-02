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
    public class ReadModelProxies
    {
        ITypeDiscoverer _typeDiscoverer;
        IEnumerable<PropertyInfo> _baseProperties = typeof(IReadModel).GetProperties();

        public ReadModelProxies(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
        }


        public string Generate()
        {
            var first = true;
            var builder = new StringBuilder();
            var types = _typeDiscoverer.FindMultiple(typeof(IReadModel));
            builder.AppendLine("Bifrost.namespace(\"readModels\", {");

            foreach (var type in types)
            {
                if (!first) builder.Append(",\n");
                var name = type.Name.ToCamelCase();

                builder.AppendFormat("\t{0} : Bifrost.read.ReadModel.extend(function() {{\n", name);
				builder.AppendFormat("\t\tvar self = this;\n");
                builder.AppendFormat("\t\tthis.name = '{0}';\n", name);

                var properties = GetPropertiesForReadModel(type);
                foreach (var property in properties)
                {
                    if (property.PropertyType.HasInterface(typeof(IDictionary<,>)) ||
                        property.PropertyType.HasInterface<IDictionary>())
                    {
						continue;
                    }
                    else if ((property.PropertyType.HasInterface(typeof(IEnumerable<>)) ||
                              property.PropertyType.HasInterface<IEnumerable>()) && property.PropertyType != typeof(string))
                    {
						continue;
                    }

					builder.AppendFormat("\t\tthis.by{0} = function(value) {{\n", property.Name.ToPascalCase());
					builder.AppendFormat("\t\t\treturn self.by(\"{0}\",value);\n",property.Name.ToCamelCase());
					builder.AppendLine("\t\t}");
                }

                builder.AppendFormat("\t}})");

                first = false;
            }

            builder.AppendLine("\n});");

            return builder.ToString();
        }

        IEnumerable<PropertyInfo> GetPropertiesForReadModel(Type type)
        {
            var properties = type.GetProperties().Where(p => !_baseProperties.Select(pi => pi.Name).Contains(p.Name));
            return properties;
        }

    }
}
