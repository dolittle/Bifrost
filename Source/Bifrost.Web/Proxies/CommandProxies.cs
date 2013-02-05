using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Concepts;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Web.Proxies
{
    public class CommandProxies
    {
        ITypeDiscoverer _typeDiscoverer;
        IEnumerable<PropertyInfo> _baseProperties = typeof(ICommand).GetProperties();

        static List<string> _namespacesToExclude = new List<string>();

        public static void ExcludeCommandsStartingWithNamespace(string @namespace)
        {
            _namespacesToExclude.Add(@namespace);
        }


        public CommandProxies(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
        }

        public string Generate()
        {
            var first = true;
            var builder = new StringBuilder();
            var types = _typeDiscoverer.FindMultiple<ICommand>().Where(t => !_namespacesToExclude.Any(n => t.Namespace.StartsWith(n)));
            builder.AppendLine("Bifrost.namespace(\"commands\", {");
            foreach (var type in types)
            {
                if (type.IsGenericType) continue;
                if (!first) builder.Append(",\n");

                var name = type.Name.ToCamelCase();

                if (name == "delete") name = "_delete";


                builder.AppendFormat("\t{0} : Bifrost.commands.Command.extend(function() {{\n", name);
                builder.AppendFormat("\t\tthis.name = '{0}';\n", name);

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

        bool AppendProperty(StringBuilder builder, PropertyInfo property, int level)
        {
            var subProperty = level > 1;

            var thisString = subProperty ? string.Empty : "this.";
            var valueAssignment = subProperty ? ":" : "=";
            var lineEnding = subProperty ? string.Empty : ";\n";

            var propertyName = property.Name.ToCamelCase();

            for (var i = 1; i < level; i++) builder.Append("\t");

            if (property.PropertyType.HasInterface(typeof(IDictionary<,>)) ||
                property.PropertyType.HasInterface<IDictionary>())
            {
                builder.AppendFormat("\t\t{0}{1} {2} {{}}{3}", thisString, propertyName,valueAssignment, lineEnding);
            }
            else if ((property.PropertyType.HasInterface(typeof(IEnumerable<>)) ||
                      property.PropertyType.HasInterface<IEnumerable>()) && property.PropertyType != typeof(string))
            {
                builder.AppendFormat("\t\t{0}{1} {2} ko.observableArray(){3}", thisString, propertyName, valueAssignment, lineEnding);
            }
            else if (   property.PropertyType.IsValueType ||
                        property.PropertyType == typeof(string) ||
                        property.PropertyType == typeof(Type) ||
                        property.PropertyType == typeof(MethodInfo) ||
                        property.PropertyType == typeof(Guid) ||
                        property.PropertyType.IsNullable() ||
                        property.PropertyType.IsConcept())
            {
                builder.AppendFormat("\t\t{0}{1} {2} ko.observable(){3}", thisString, propertyName,valueAssignment, lineEnding);
            }
            else
            {
                builder.AppendFormat("\t\t{0}{1} {2} {{\n", thisString, propertyName,valueAssignment);

                var first = true;
                foreach (var childProperty in property.PropertyType.GetProperties()) 
                {
                    if( !first )
                        builder.Append(",\n");

                    var appended = AppendProperty(builder, childProperty, level+1);
                    if (appended)
                    {
                        if (first)
                            first = false;
                    }
                }

                builder.Append("\n");

                for (var i = 1; i < level; i++) builder.Append("\t");
                builder.Append("\t\t}");
                if (!subProperty) builder.Append(";");
                builder.Append("\n");
            }

            return true;
        }

        IEnumerable<PropertyInfo> GetPropertiesForCommand(Type type)
        {
            var properties = type.GetProperties().Where(p => !_baseProperties.Select(pi=>pi.Name).Contains(p.Name));
            return properties;
        }
    }
}
