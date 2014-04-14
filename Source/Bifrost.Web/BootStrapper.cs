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

using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.Web.Assets;
using Bifrost.Web.Commands;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;
using Bifrost.Web.Read;
using Bifrost.Web.Sagas;
using Bifrost.Web.Security;
using Bifrost.Web.Services;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.BootStrapper),"PreApplicationStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(Bifrost.Web.BootStrapper), "Start")]

namespace Bifrost.Web
{
	public class BootStrapper
	{
        static volatile object _lockObject = new object();
	    static bool _isInitialized;

		static void PreApplicationStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(HttpModule));
            RouteTable.Routes.Add(new ProxyRoute());
            RouteTable.Routes.Add(new SecurityRoute());
            RouteTable.Routes.Add(new ConfigurationRoute());
            RouteTable.Routes.Add(new AssetManagerRoute("Bifrost/AssetsManager"));
            RouteTable.Routes.AddService<CommandCoordinatorService>("Bifrost/CommandCoordinator");
            RouteTable.Routes.AddService<CommandSecurityService>("Bifrost/CommandSecurity");
            RouteTable.Routes.AddService<SagaNarratorService>("Bifrost/SagaNarrator");
            RouteTable.Routes.AddService<QueryService>("Bifrost/Query");
            RouteTable.Routes.AddService<ReadModelService>("Bifrost/ReadModel");
            RouteTable.Routes.AddApplicationFromAssembly("Bifrost", typeof(BootStrapper).Assembly);
        }

        public static void Start()
        {
            lock (_lockObject)
            {
                if (_isInitialized) return;
                
                Configure.DiscoverAndConfigure();
                AddAllAssetsForThisAssembly();

                _isInitialized = true;
            }
        }

        // TODO: this is just a temporary solution for this particular Web Application - we need to revisit this whole thing so that any applications added from an assembly gets their assets relative to their route registered!
        // it probably needs formalizing the AssetsManager a bit more!
        static void AddAllAssetsForThisAssembly()
        {
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();

            var rootNamespace = typeof(BootStrapper).Namespace;
            var resources = typeof(BootStrapper).Assembly.GetManifestResourceNames();
            foreach (var resource in resources)
            {
                var resourceName = resource.Replace(rootNamespace + ".", string.Empty);
                resourceName = resourceName.Replace(".", "/");
                resourceName = "Bifrost/" + resourceName;
                var formatted = string.Format("{0}.{1}",
                    resourceName.Substring(0, resourceName.LastIndexOf("/")),
                    resourceName.Substring(resourceName.LastIndexOf("/") + 1)
                    );

                assetsManager.AddAsset(formatted);
            }
        }
	}
}

