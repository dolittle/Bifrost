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
using System;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Security;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents the configuration for security
    /// </summary>
    public class SecurityConfiguration : ISecurityConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public void Initialize(IContainer container)
        {
            var typeDiscoverer = container.Get<ITypeDiscoverer>();

            var resolverType = typeof(DefaultPrincipalResolver);
            var resolverTypes = typeDiscoverer.FindMultiple<ICanResolvePrincipal>().Where(t => t.Assembly != typeof(SecurityConfiguration).Assembly);
            if (resolverTypes.Count() > 1) throw new MultiplePrincipalResolversFound();
            if (resolverTypes.Count() == 1) resolverType = resolverTypes.First();

            container.Bind<ICanResolvePrincipal>(resolverType);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
