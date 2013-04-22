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
using System.Reflection;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Proxies
{
    public class NamespaceConfigurationProxies : IProxyGenerator
    {
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public NamespaceConfigurationProxies(WebConfiguration configuration, ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var global = _codeGenerator
                .Global()
                    .Variant("namespaceMapper", v => v.WithFunctionCall(f=>f.WithName("Bifrost.StringMapper.create")))
                    .WithNamespaceMappersFrom(_configuration.Namespaces)
                    .AssignAccessor("Bifrost.namespaceMappers.default", a => a.WithLiteral("namespaceMapper"))
                    ;

            var result = _codeGenerator.GenerateFrom(global);
            return result;
        }
    }
}
