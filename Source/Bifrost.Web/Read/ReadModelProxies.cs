/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Text;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Read
{
    public class ReadModelProxies : IProxyGenerator
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ICodeGenerator _codeGenerator;
        readonly WebConfiguration _configuration;

        public ReadModelProxies(
            ITypeDiscoverer typeDiscoverer,
            ICodeGenerator codeGenerator,
            WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        string ClientNamespace(string @namespace)
        {
            return _configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace) ?? Namespaces.READ;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer
                .FindMultiple<IReadModel>()
                .OrderBy(t => t.FullName)
                .GroupBy(t => ClientNamespace(t.Namespace))
                .OrderBy(n => n.Key);
            var result = new StringBuilder();

            foreach (var @namespace in typesByNamespace)
            {
                var currentNamespace = _codeGenerator.Namespace(@namespace.Key);
                foreach (var type in @namespace)
                {
                    var name = type.Name.ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("Bifrost.read.ReadModel")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))
                                        .WithPropertiesFrom(type, typeof(IReadModel)));

                    currentNamespace.Content.Assign("readModelOf" + name.ToPascalCase())
                        .WithType(t =>
                            t.WithSuper("Bifrost.read.ReadModelOf")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_name", p => p.WithString(name))
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))
                                        .Property("_readModelType", p => p.WithLiteral(currentNamespace.Name+"." + name))
                                        .WithReadModelConvenienceFunctions(type));
                }

                result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }

            return result.ToString();
        }
    }
}
