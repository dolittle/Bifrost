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
using System.Collections.Generic;
using System.Linq;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Web.Proxies
{
    public class CommandProxies : IProxyGenerator
    {
        static List<string> _namespacesToExclude = new List<string>();

        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;

        public static void ExcludeCommandsStartingWithNamespace(string @namespace)
        {
            _namespacesToExclude.Add(@namespace);
        }

        public CommandProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;

        }

        public string Generate()
        {
            var types = _typeDiscoverer.FindMultiple<ICommand>().Where(t => !_namespacesToExclude.Any(n => t.Namespace.StartsWith(n)));
            var ns = _codeGenerator.Namespace("commands",
                o =>
                {
                    foreach (var type in types)
                    {
                        if (type.IsGenericType) continue;
                        var name = type.Name.ToCamelCase();
                        o.Assign(name)
                            .WithType(t =>
                                t.WithSuper("Bifrost.commands.Command")
                                    .Function
                                        .Body
                                            .Variant("self", v => v.WithThis())
                                            .Property("name", p => p.WithString(name))
                                            .WithObservablePropertiesFrom(type, typeof(ICommand)));
                    }
                });

            var result = _codeGenerator.GenerateFrom(ns);
            return result;
        }
    }
}
