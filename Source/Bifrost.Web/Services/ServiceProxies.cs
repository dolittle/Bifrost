/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Services
{
    public class ServiceProxies : IProxyGenerator
    {
        ICodeGenerator _codeGenerator;
        IRegisteredServices _registeredServices;


        public ServiceProxies(IRegisteredServices registeredServices, ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
            _registeredServices = registeredServices;
        }  


        public string Generate()
        {
            var serviceRegistrations = _registeredServices.GetAll();
            var ns = _codeGenerator.Namespace("services",
                o =>
                {
                    foreach (var serviceRegistration in serviceRegistrations)
                    {
                        var name = serviceRegistration.Key.Name.ToCamelCase();
                        o.Assign(name)
                            .WithType(t =>
                                t.WithSuper("Bifrost.services.Service")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .Property("name", p => p.WithString(name))
                                            .Property("url", p=> p.WithString(serviceRegistration.Value))
                                            .WithServiceMethodsFrom(serviceRegistration.Key));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
            
        }
    }
}
