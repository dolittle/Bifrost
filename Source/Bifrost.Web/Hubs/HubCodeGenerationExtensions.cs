#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;

namespace Bifrost.Web.Hubs
{
    public static class HubCodeGenerationExtensions
    {
        public static FunctionBody WithServerPropertiesFrom(this FunctionBody body, Type type)
        {
            body.Property("server", p => p.WithObjectLiteral(o =>
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var method in methods)
                {
                    if (method.Name.StartsWith("get_") || method.Name.StartsWith("set_")) continue;

                    o.Assign(method.Name.ToCamelCase()).WithFunction(f =>
                    {
                        f.Body.Access("self", a => a.WithFunctionCall(fc => fc
                            .WithName("invokeServerMethod")
                            .WithParameters("\"" + method.Name + "\"", "arguments")));
                    });

                }
            }));

            return body;
        }
    }
}
