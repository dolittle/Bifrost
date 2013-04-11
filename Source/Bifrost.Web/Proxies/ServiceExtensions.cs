#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Linq;
using Bifrost.Extensions;
using Bifrost.CodeGeneration.JavaScript;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Bifrost.Web.Proxies
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
