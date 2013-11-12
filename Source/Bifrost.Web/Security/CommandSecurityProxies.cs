using System;
using System.Linq;
using System.Text;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Security
{
    public class CommandSecurityProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ITypeImporter _typeImporter;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public CommandSecurityProxies(ITypeDiscoverer typeDiscoverer, ITypeImporter typeImporter, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _typeImporter = typeImporter;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer.FindMultiple<ICommand>().Where(t => !CommandProxies._namespacesToExclude.Any(n => t.Namespace.StartsWith(n))).GroupBy(t => t.Namespace);
            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalCommands = _codeGenerator.Namespace(Namespaces.COMMANDS);

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                else
                    currentNamespace = globalCommands;

                foreach (var type in @namespace)
                {
                    if (type.IsGenericType) continue;
                    var name = string.Format("{0}SecurityContext",type.Name.ToCamelCase());
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("Bifrost.commands.CommandSecurityContext")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Call(f => f.WithName("isAuthorized").WithParameters("false"))
                            );

                }
            }
            result.Append(_codeGenerator.GenerateFrom(globalCommands));
            
            return result.ToString();
        }
    }
}
