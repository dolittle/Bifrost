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

using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
	/// <summary>
	/// Represents a <see cref="IDefaultConventions"/> implementation
	/// </summary>
    public class DefaultConventions : IDefaultConventions
	{
		IContainer _container;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Bifrost.Configuration.Defaults.DefaultConventions"/> class.
		/// </summary>
		public DefaultConventions(IContainer container)
		{
			_container = container;
			_container.Bind<IBindingConventionManager>(typeof(BindingConventionManager));
		}
		
#pragma warning disable 1591 // Xml Comments
		public void Initialize()
        {
            var conventionManager = _container.Get<IBindingConventionManager>();
            conventionManager.Add<DefaultConvention>();
            conventionManager.DiscoverAndInitialize();
		}
#pragma warning restore 1591 // Xml Comments
	}
}