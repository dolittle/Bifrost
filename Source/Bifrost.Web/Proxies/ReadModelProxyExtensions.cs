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
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Proxies
{
    public static class ReadModelProxyExtensions
    {
        public static Container WithReadModelConvenienceFunctions(this FunctionBody functionBody, Type type)
        {
            var excludePropertiesFrom = typeof(IReadModel);
            var properties = type.GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

            foreach (var property in properties)
            {
                var functionName = string.Format("matching{0}",property.Name.ToPascalCase());
                var propertyName = property.Name.ToCamelCase();
                var filter = new ObjectLiteral();
                filter.Assign(propertyName).WithLiteral(propertyName);

                functionBody.Property(functionName,p =>
                    p.WithFunction(function => 
                        function
                            .WithParameters(propertyName)
                                .Body
                                    .Scope("self", scope=>
                                        scope.FunctionCall(f=>f.WithName("instanceMatching").WithParameters(new[] { filter })
                                    )
                                )
                            )
                        );
            }

            return functionBody;
        }
    }
}
