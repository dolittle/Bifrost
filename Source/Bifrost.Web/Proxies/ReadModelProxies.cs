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
using System.Linq;
using System.Text;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ReadModelProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public ReadModelProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer.FindMultiple<IReadModel>().GroupBy(t => t.Namespace);

            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalCommands = _codeGenerator.Namespace("commands");

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMappers.HasMappingFor(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMappers.Resolve(@namespace.Key));
                else
                    currentNamespace = globalCommands;


                foreach (var type in @namespace)
                {
                    var name = type.Name.ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("Bifrost.read.ReadModel")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .WithPropertiesFrom(type, typeof(IReadModel)));

                    currentNamespace.Content.Assign("readModelOf" + name.ToPascalCase())
                        .WithType(t =>
                            t.WithSuper("Bifrost.read.ReadModelOf")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("name", p => p.WithString(name))
                                        .Property("readModelType", p => p.WithLiteral("readModels." + name))
                                        .WithReadModelConvenienceFunctions(type));
                }

                if (currentNamespace != globalCommands)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }
            result.Append(_codeGenerator.GenerateFrom(globalCommands));
            return result.ToString();
        }
    }
}
