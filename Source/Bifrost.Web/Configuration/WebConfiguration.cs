#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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

using Bifrost.Configuration;
using Bifrost.Execution;

namespace Bifrost.Web.Configuration
{
    public class WebConfiguration : IFrontendTargetConfiguration
    {
        public WebConfiguration(NamespaceMapper namespaceMapper)
        {
            ScriptsToInclude = new ScriptsToInclude();
            PathsToNamespaces = new PathToNamespaceMappers();
            NamespaceMapper = namespaceMapper;
        }

        public ScriptsToInclude ScriptsToInclude { get; set; }
        public PathToNamespaceMappers PathsToNamespaces { get; set; }
        public NamespaceMapper NamespaceMapper { get; set; }
        public bool ApplicationRouteCached { get; set; }

        public void Initialize(IContainer container)
        {
        }
    }
}
