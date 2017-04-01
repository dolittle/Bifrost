/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;

namespace Bifrost.Web.Hubs
{
    public static class HubCodeGenerationExtensions
    {
        public static FunctionBody WithServerMethodsFrom(this FunctionBody body, Type type)
        {
            body.Property("server", p => p.WithObjectLiteral(o =>
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var method in methods)
                {
                    if (method.Name.StartsWith("get_") || method.Name.StartsWith("set_")) continue;

                    o.Assign(method.Name.ToCamelCase()).WithFunction(f =>
                    {
                        f.Body.Variant("result", v =>
                        {
                            v.WithFunctionCall(fc => fc
                                .WithName("self.invokeServerMethod")
                                .WithParameters("\"" + method.Name + "\"", "arguments")
                            );
                        });

                        f.Body.Return(new Literal("result"));
                    });

                }
            }));

            return body;
        }
    }
}
