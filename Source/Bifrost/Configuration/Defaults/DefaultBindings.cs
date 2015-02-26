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
using Bifrost.Configuration.Assemblies;
using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
	/// <summary>
	/// Represents a <see cref="IDefaultBindings"/>
	/// </summary>
    public class DefaultBindings : IDefaultBindings
	{
        AssembliesConfiguration _assembliesConfiguration;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultBindings"/>
        /// </summary>
        public DefaultBindings(AssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
        }

#pragma warning disable 1591 // Xml Comments
		public void Initialize(IContainer container)
        {
            container.Bind(container);
#if(!SILVERLIGHT)
            container.Bind<_AppDomain>(AppDomain.CurrentDomain);
#endif
            container.Bind<AssembliesConfiguration>(_assembliesConfiguration);
            container.Bind<IAssemblyProvider>(typeof(AssemblyProvider), BindingLifecycle.Singleton);
            container.Bind<IAssemblyFilters>(typeof(AssemblyFilters), BindingLifecycle.Singleton);
            container.Bind<IAssemblies>(typeof(global::Bifrost.Execution.Assemblies), BindingLifecycle.Singleton);
            container.Bind<ITypeDiscoverer>(typeof(TypeDiscoverer), BindingLifecycle.Singleton);
		}
#pragma warning restore 1591 // Xml Comments
	}
}