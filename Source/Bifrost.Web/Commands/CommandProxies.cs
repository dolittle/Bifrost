/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Commands
{
    public class CommandProxies : IProxyGenerator
    {
        internal static List<string> NamespacesToExclude = new List<string>();

        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ITypeImporter _typeImporter;
        readonly ICodeGenerator _codeGenerator;
        readonly WebConfiguration _configuration;

        public static void ExcludeCommandsStartingWithNamespace(string @namespace)
        {
            NamespacesToExclude.Add(@namespace);
        }

        public CommandProxies(
            ITypeDiscoverer typeDiscoverer,
            ITypeImporter typeImporter,
            ICodeGenerator codeGenerator,
            WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _typeImporter = typeImporter;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        string ClientNamespace(string @namespace)
        {
            return _configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace) ?? Namespaces.COMMANDS;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer
                .FindMultiple<ICommand>()
                .Where(t => !t.IsGenericType)
                .Where(t => !NamespacesToExclude.Any(n => t.Namespace.StartsWith(n)))
                .OrderBy(t => t.FullName)
                .GroupBy(t => ClientNamespace(t.Namespace))
                .OrderBy(n => n.Key);
            var commandPropertyExtenders = _typeImporter.ImportMany<ICanExtendCommandProperty>();
            var result = new StringBuilder();

            foreach (var @namespace in typesByNamespace)
            {
                var currentNamespace = _codeGenerator.Namespace(@namespace.Key);
                foreach (var type in @namespace)
                {
                    var name = type.Name.ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("Bifrost.commands.Command")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_name", p => p.WithString(name))
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))

                                        .WithObservablePropertiesFrom(type, excludePropertiesFrom: typeof(ICommand), observableVisitor: (propertyName, observable) =>
                                        {
                                            foreach (var commandPropertyExtender in commandPropertyExtenders)
                                            {
                                                commandPropertyExtender.Extend(type, propertyName, observable);
                                            }
                                        }));
                }

                result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }

            return result.ToString();
        }
    }
}
