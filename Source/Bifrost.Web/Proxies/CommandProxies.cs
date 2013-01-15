using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;

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
                
                builder.AppendFormat("\t{0} : Bifrost.commands.Command.extend(function() {{\n", type.Name.ToCamelCase());
                builder.AppendFormat("\t\tthis.name = '{0}';\n", type.Name.ToCamelCase());

                var properties = GetPropertiesForCommand(type);
                foreach (var property in properties)
                    builder.AppendFormat("\t\tthis.{0} = ko.observable();\n", property.Name.ToCamelCase());

                builder.Append("\t})");

                first = false;
            }
            builder.AppendLine("\n});");

            return builder.ToString();
        }


        IEnumerable<PropertyInfo> GetPropertiesForCommand(Type type)
        {
            var properties = type.GetProperties().Where(p => !_baseProperties.Select(pi=>pi.Name).Contains(p.Name));
            return properties;
        }
    }
}
