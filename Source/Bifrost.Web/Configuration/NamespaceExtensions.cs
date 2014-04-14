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
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Configuration
{
    public static class NamespaceExtensions
    {
        public static FunctionBody WithNamespaceMappersFrom(this FunctionBody global, PathToNamespaceMappers namespaceMappers)
        {
            foreach( var map in namespaceMappers.Maps ) {
                global.Access("namespaceMapper",
                    a => a.WithFunctionCall(
                        f => f.WithName("addMapping").WithParameters("\"" + map.Key + "\"", "\"" + map.Value + "\"")));
            }

            return global;
        }
    }
}
