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
using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHttpHandler : IHttpHandler
    {
        string _commandProxies;
		string _readModelProxies;
        string _queryProxies;

        public ProxyRouteHttpHandler() : 
            this(
                Configure.Instance.Container.Get<CommandProxies>(),
                Configure.Instance.Container.Get<QueryProxies>(),
				Configure.Instance.Container.Get<ReadModelProxies>()
            )
        {
        }

        public ProxyRouteHttpHandler(
			CommandProxies commandProxies, 
			QueryProxies queryProxies,
			ReadModelProxies readModelProxies)
        {
            _commandProxies = commandProxies.Generate();
			_readModelProxies = readModelProxies.Generate();
            _queryProxies = queryProxies.Generate();
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_commandProxies);
			context.Response.Write(_readModelProxies);
            context.Response.Write(_queryProxies);
        }
    }
}
