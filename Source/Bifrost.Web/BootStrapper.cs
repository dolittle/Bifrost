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

using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.Web.Assets;
using Bifrost.Web.Commands;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;
using Bifrost.Web.Read;
using Bifrost.Web.Sagas;
using Bifrost.Web.Services;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.BootStrapper),"PreApplicationStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(Bifrost.Web.BootStrapper), "PostApplicationStart")]

namespace Bifrost.Web
{
	public class BootStrapper
	{
		public static void PreApplicationStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(HttpModule));

            RouteTable.Routes.Add(new ProxyRoute());
            RouteTable.Routes.Add(new ConfigurationRoute());
            RouteTable.Routes.Add(new AssetManagerRoute("Bifrost/AssetsManager"));
            RouteTable.Routes.AddService<CommandCoordinatorService>("Bifrost/CommandCoordinator");
            RouteTable.Routes.AddService<CommandSecurityService>("Bifrost/CommandSecurity");
            RouteTable.Routes.AddService<SagaNarratorService>("Bifrost/SagaNarrator");
            RouteTable.Routes.AddService<QueryService>("Bifrost/Query");
            RouteTable.Routes.AddService<ReadModelService>("Bifrost/ReadModel");
            RouteTable.Routes.AddApplicationFromAssembly("Bifrost", typeof(BootStrapper).Assembly);
		}

        public static void PostApplicationStart()
        {
            Configure.DiscoverAndConfigure();
        }
	}
}

