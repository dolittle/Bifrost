using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using System.Diagnostics;


namespace Bifrost.Web.Proxies
{
    public class CommandProxies
    {
        ITypeDiscoverer _typeDiscoverer;
        IEnumerable<PropertyInfo> _baseProperties = typeof(ICommand).GetProperties();

        public CommandProxies(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
        }

        public string Generate()
        {
            var first = true;
            var builder = new StringBuilder();
            var types = _typeDiscoverer.FindMultiple<ICommand>();
            builder.AppendLine("Bifrost.namespace(\"commands\", {");
            foreach (var type in types)
            {
                if (type.IsGenericType) continue;
                if (!first) builder.Append(",\n");

                var name = type.Name.ToCamelCase();

                if (name == "delete") name = "_delete";


                builder.AppendFormat("\t{0} : Bifrost.commands.Command.extend(function() {{\n", name);
                builder.AppendFormat("\t\tthis.name = '{0}';\n", name);

                Debug.WriteLine("CommandType : "+type);
                var properties = GetPropertiesForCommand(type);
                foreach (var property in properties)
                {
                    AppendProperty(builder, property,1);
                }

                builder.Append("\t})");

                first = false;
            }
            builder.AppendLine("\n});");

            return builder.ToString();
        }

        void AppendProperty(StringBuilder builder, PropertyInfo property, int level)
        {
            for (var i = 0; i < level; i++) Debug.Write("\t");
            Debug.Write("Property : " + property.Name + ", Type : " + property.PropertyType.Name);
            Debug.Write("\n");
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
            else if (   property.PropertyType.IsValueType ||
                        property.PropertyType == typeof(string) ||
                        property.PropertyType == typeof(Type) ||
                        property.PropertyType == typeof(MethodInfo) ||
                        property.PropertyType == typeof(Guid) ||
                        property.PropertyType.IsNullable() ||
                        property.PropertyType.IsConcept())
            {
                builder.AppendFormat("\t\tthis.{0} = ko.observable();\n", property.Name.ToCamelCase());
            }
            else
            {
                foreach (var childProperty in property.PropertyType.GetProperties())
                    AppendProperty(builder, childProperty, level+1);
            }
        }

        IEnumerable<PropertyInfo> GetPropertiesForCommand(Type type)
        {
            var properties = type.GetProperties().Where(p => !_baseProperties.Select(pi=>pi.Name).Contains(p.Name));
            return properties;
        }
    }
}
