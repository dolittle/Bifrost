/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;

namespace Bifrost.Web.Services
{
    public static class ServiceExtensions
    {
        public static Container WithServiceMethodsFrom(this FunctionBody functionBody, Type type)
        {
            var objectMethods = typeof(object).GetMethods();
            var methods = type.GetMethods(BindingFlags.Public|BindingFlags.Instance).Where(m => objectMethods.Any(om=>om.DeclaringType != m.DeclaringType));

            foreach (var method in methods)
            {
                var functionName = method.Name.ToCamelCase();

                var selfScopeCall = new Scope("self");
                
                var parameters = method.GetParameters().Select(p => p.Name.ToCamelCase()).ToArray();
                var objectLiteral = new ObjectLiteral();
                foreach (var parameter in parameters) 
                    objectLiteral.Assign(parameter).WithLiteral(parameter);


                if (method.ReturnType == typeof(void))
                    selfScopeCall.FunctionCall(f => f.WithName("callWithoutReturnValue").WithParameters(new Literal("\"" + method.Name + "\""), objectLiteral));
                else if (method.ReturnType.IsDictionary() || !method.ReturnType.IsEnumerable())
                    selfScopeCall.FunctionCall(f => f.WithName("callWithObjectAsReturn").WithParameters(new Literal("\"" + method.Name + "\""), objectLiteral));
                else if (method.ReturnType.IsEnumerable())
                    selfScopeCall.FunctionCall(f => f.WithName("callWithArrayAsReturn").WithParameters(new Literal("\"" + method.Name + "\""), objectLiteral));

                functionBody.Property(functionName, p =>
                {
                    p.WithFunction(function =>
                        function
                            .WithParameters(parameters)
                                .Body
                                    .Return(selfScopeCall)
                    );
                });
            }

            return functionBody;
        }

        static bool IsEnumerable(this Type type)
        {
            return (type.HasInterface(typeof(IEnumerable<>)) ||
                   type.HasInterface<IEnumerable>()) && type != typeof(string);
        }

        static bool IsDictionary(this Type type)
        {
            return type.HasInterface(typeof(IDictionary<,>)) ||
                    type.HasInterface<IDictionary>();
        }


    }
}
