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

namespace Bifrost.Web.Applications
{
    public class AssetManagerRouteHttpHandler : IHttpHandler
    {
        string _url;
        bool _initialized = false;
        Dictionary<string, List<string>> _assetsByExtension = new Dictionary<string, List<string>>();

        public AssetManagerRouteHttpHandler(string url)
        {
            _url = url;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            InitializeIfNotInitialized(context);
            var assets = new List<string>();
            var extension = context.Request.Params["extension"];
            if( extension != null ) 
            {
                extension = "." + extension;
                if (_assetsByExtension.ContainsKey(extension))
                    assets = _assetsByExtension[extension];
            }

            if (context.Request.Params["structure"] != null)
                assets = assets.Select(a => FormatPath(Path.GetDirectoryName(a))).Distinct().ToList();

            var serialized = JsonConvert.SerializeObject(assets);
            context.Response.Write(serialized);
        }

        string FormatPath(string input)
        {
            return input.Replace("\\", "/");
        }

        void InitializeIfNotInitialized(HttpContext context)
        {
            if (_initialized)
                return;

            _initialized = true;

            var root = context.Server.MapPath("/");
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                var relativePath = FormatPath(file.Replace(root, string.Empty));

                List<string> assets;
                if (!_assetsByExtension.ContainsKey(extension))
                {
                    assets = new List<string>();
                    _assetsByExtension[extension] = assets;
                }
                else
                    assets = _assetsByExtension[extension];

                assets.Add(relativePath);
            }
        }
    }
}
