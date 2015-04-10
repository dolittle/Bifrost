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
using Bifrost.CommonServiceLocator;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Provides extensions for configuration
    /// </summary>
    public static class ConfigurationExtensions
    {
        static bool _configured = false;

        /// <summary>
        /// Enables common service locator and sets the <see cref="IContainer">container</see> as the source of location
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> object to configure</param>
        /// <returns>Chained <see cref="IConfigure"/></returns>
        public static IConfigure UsingCommonServiceLocator(this IConfigure configure)
        {
            if (!_configured)
            {
                var serviceLocator = new ContainerServiceLocator(configure.Container);
                configure.Container.Bind<IServiceLocator>(serviceLocator);
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                _configured = true;
            }
            return Configure.Instance;
        }
    }
}
