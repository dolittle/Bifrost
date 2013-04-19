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
using Bifrost.JSON.Serialization;
using Bifrost.SignalR;
using Bifrost.SignalR.Events;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this IConfigure configure)
        {
            GlobalHost.DependencyResolver = new BifrostDependencyResolver(Configure.Instance.Container);
            RouteTable.Routes.MapHubs();

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new FilteredCamelCasePropertyNamesContractResolver(),
                Converters = { new ConceptConverter(), new ConceptDictionaryConverter() }
            };
            var jsonNetSerializer = new JsonNetSerializer(serializerSettings);
            GlobalHost.DependencyResolver.Register(typeof(IJsonSerializer), () => jsonNetSerializer); 

            return Configure.Instance;
        }

        public static IConfigure UsingSignalR(this IEventsConfiguration configuration)
        {
            configuration.AddEventStoreChangeNotifier(typeof(EventStoreChangeNotifier));
            return Configure.Instance;
        }
    }
}
