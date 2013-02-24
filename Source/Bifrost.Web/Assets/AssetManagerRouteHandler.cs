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
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Assets
{
    public class AssetManagerRouteHandler : IRouteHandler
    {
        string _url;
        IHttpHandler _httpHandler;

        public AssetManagerRouteHandler(string url)
        {
            _url = url;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (_httpHandler == null)
                _httpHandler = new AssetManagerRouteHttpHandler(_url);

            return _httpHandler;
        }
    }
}
