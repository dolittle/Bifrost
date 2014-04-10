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
using System.Text;
using System.Linq;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Read
{
    public class QueryProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public QueryProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer.FindMultiple(typeof(IQueryFor<>)).GroupBy(t => t.Namespace);

            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalRead = _codeGenerator.Namespace(Namespaces.READ);

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                else
                    currentNamespace = globalRead;

                foreach (var type in @namespace)
                {
                    var name = type.Name.ToCamelCase();
                    var queryForTypeName = type.GetInterface(typeof(IQueryFor<>).Name).GetGenericArguments()[0].Name.ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("Bifrost.read.Query")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("name", p => p.WithString(name))
                                        .Property("generatedFrom", p => p.WithString(type.FullName))
                                        .Property("readModel", p => p.WithLiteral(currentNamespace.Name + "." + queryForTypeName))
                                        .WithObservablePropertiesFrom(type, excludePropertiesFrom: typeof(IQueryFor<>), propertyVisitor: (p) => p.Name != "Query"));

                }
                if (currentNamespace != globalRead)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }

            result.Append(_codeGenerator.GenerateFrom(globalRead));
            return result.ToString();
        }

    }
}
