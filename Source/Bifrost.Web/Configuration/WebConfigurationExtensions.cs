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
using System;
using Bifrost.Web;
using Bifrost.Web.Configuration;
using Bifrost.Web.Pipeline;
using Bifrost.Web.Services;

namespace Bifrost.Configuration
{
	public static class WebConfigurationExtensions
	{
        public static IConfigure Web(this IFrontendConfiguration configuration, Action<WebConfiguration> configureCallback)
        {
            var webConfiguration = new WebConfiguration(Configure.Instance.Container.Get<NamespaceMapper>());
            Configure.Instance.Container.Bind<WebConfiguration>(webConfiguration);
            configuration.Target = webConfiguration;
            configureCallback(webConfiguration);
            return Configure.Instance;
        }

        public static WebConfiguration CacheApplicationRoute(this WebConfiguration configuration)
        {
            configuration.ApplicationRouteCached = true;
            return configuration;
        }

		public static WebConfiguration AsSinglePageApplication(this WebConfiguration configuration)
		{
			HttpModule.AddPipe(new SinglePageApplication());
			return configuration;
		}

        public static WebConfiguration WithoutJQuery(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.JQuery = false;
            return configuration;
        }

        public static WebConfiguration WithoutJQueryHistory(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.JQueryHistory = false;
            return configuration;
        }

        public static WebConfiguration WithoutKnockout(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.Knockout = false;
            return configuration;
        }

        public static WebConfiguration WithoutRequire(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.Require = false;
            return configuration;
        }

        public static WebConfiguration Namespaces(this WebConfiguration configuration, Action<PathToNamespaceMappers> callback)
        {
            callback(configuration.PathsToNamespaces);
            return configuration;
        }
	}
}

