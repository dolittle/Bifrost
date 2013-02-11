using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Proxies
{
    public class QueryProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;

        public QueryProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
        }

        public string Generate()
        {
            var types = _typeDiscoverer.FindMultiple(typeof(IQueryFor<>));
            var ns = _codeGenerator.Namespace("queries",
                o =>
                {
                    foreach (var type in types)
                    {
                        var name = type.Name.ToCamelCase();
                        o.Assign(name)
                            .WithType(t =>
                                t.WithSuper("Bifrost.read.Query")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .Property("name", p => p.WithString(name))
                                            .WithPropertiesFrom(type, typeof(IQueryFor<>)));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
        }

    }
}
