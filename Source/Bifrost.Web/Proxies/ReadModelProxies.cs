using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Web.Proxies.JavaScript;

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
                                            .WithPropertiesFrom(type, typeof(IReadModel), a => a.Name = "by" + a.Name.ToPascalCase()));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
        }
    }
}
