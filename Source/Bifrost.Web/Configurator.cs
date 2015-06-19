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
using System.Linq;
using System.Web.Routing;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.JSON.Concepts;
using Bifrost.JSON.Serialization;
using Bifrost.Web.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Owin;

namespace Bifrost.Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            configure.CallContext.WithCallContextTypeOf<WebCallContext>();
            ConfigureSignalR(configure);
        }

        void ConfigureSignalR(IConfigure configure)
        {
            var resolver = new BifrostDependencyResolver(configure.Container);

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new FilteredCamelCasePropertyNamesContractResolver(),
                Converters = { new ConceptConverter(), new ConceptDictionaryConverter() }
            };
            var jsonSerializer = JsonSerializer.Create(serializerSettings);
            resolver.Register(typeof(JsonSerializer), () => jsonSerializer);

            GlobalHost.DependencyResolver = resolver;

            var hubConfiguration = new HubConfiguration { Resolver = resolver };

            RouteTable.Routes.MapOwinPath("/signalr", a => a.RunSignalR(hubConfiguration));
            var route = RouteTable.Routes.Last();
            RouteTable.Routes.Remove(route);
            RouteTable.Routes.Insert(0, route);
        }
    }
}
