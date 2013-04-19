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
using System.Linq;
using System.Reflection;
using System.Web;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Bifrost.Configuration;

namespace Bifrost.Web.Assets
{
    public class AssetManagerRouteHttpHandler : IHttpHandler
    {
        string _url;
        IAssetsManager _assetsManager;

        public AssetManagerRouteHttpHandler(string url) :
            this(url, Configure.Instance.Container.Get<IAssetsManager>()) 
        { 
        }
        

        public AssetManagerRouteHttpHandler(string url, IAssetsManager assetsManager)
        {
            _url = url;
            _assetsManager = assetsManager;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            IEnumerable<string> assets = new string[0];
            var extension = context.Request.Params["extension"];
            if( extension != null ) 
            {
                assets = _assetsManager.GetFilesForExtension(extension);
                if (context.Request.Params["structure"] != null)
                    assets = _assetsManager.GetStructureForExtension(extension);
            }
            var serialized = JsonConvert.SerializeObject(assets);
            context.Response.Write(serialized);
        }
    }
}
