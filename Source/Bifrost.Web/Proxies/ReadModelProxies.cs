using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Proxies
{
    public class ReadModelProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;

        public ReadModelProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
        }

        public string Generate()
        {
            var types = _typeDiscoverer.FindMultiple(typeof(IReadModel));

            var ns = _codeGenerator.Namespace("readModels",
                o =>
                {
                    foreach (var type in types)
                    {
                        var name = type.Name.ToCamelCase();
                        o.Assign(name)
                            .WithType(t =>
                                t.WithSuper("Bifrost.read.ReadModel")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .WithPropertiesFrom(type, typeof(IReadModel)));

                        o.Assign("readModelOf" + name.ToPascalCase())
                            .WithType(t =>
                                t.WithSuper("Bifrost.read.ReadModelOf")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .Property("name", p => p.WithString(name))
                                            .WithReadModelConvenienceFunctions(type));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
        }
    }
}
