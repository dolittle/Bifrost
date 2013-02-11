using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Web.Proxies.JavaScript;

namespace Bifrost.Web.Proxies
{
    public class CommandProxies : IProxyGenerator
    {
        static List<string> _namespacesToExclude = new List<string>();

        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;

        public static void ExcludeCommandsStartingWithNamespace(string @namespace)
        {
            _namespacesToExclude.Add(@namespace);
        }

        public CommandProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;

        }

        public string Generate()
        {
            var types = _typeDiscoverer.FindMultiple<ICommand>().Where(t => !_namespacesToExclude.Any(n => t.Namespace.StartsWith(n)));
            var ns = _codeGenerator.Namespace("commands",
                o =>
                {
                    foreach (var type in types)
                    {
                        if (type.IsGenericType) continue;
                        var name = type.Name.ToCamelCase();
                        o.Assign(name)
                            .WithType(t =>
                                t.WithSuper("Bifrost.commands.Command")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .Property("name", p => p.WithString(name))
                                            .WithPropertiesFrom(type, typeof(ICommand)));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
        }
    }
}
