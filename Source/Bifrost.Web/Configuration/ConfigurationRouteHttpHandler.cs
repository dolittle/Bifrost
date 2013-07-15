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
using System.Text;
using System.Web;
using Bifrost.Configuration;
using Bifrost.Web.Assets;
using Bifrost.Web.Proxies;
using Newtonsoft.Json;

namespace Bifrost.Web.Configuration
{
    public class ConfigurationRouteHttpHandler : IHttpHandler
    {
        string _configurationAsString;

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            InitializeIfNotInitialized();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_configurationAsString);
        }


        string GetResource(string name)
        {
            var stream = typeof(ConfigurationRouteHttpHandler).Assembly.GetManifestResourceStream(name);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            var content = UTF8Encoding.UTF8.GetString(bytes);
            return content;
        }

        void InitializeIfNotInitialized()
        {
            if (!string.IsNullOrEmpty(_configurationAsString)) return;

            var proxies = Configure.Instance.Container.Get<GeneratedProxies>();
            var configuration = Configure.Instance.Container.Get<WebConfiguration>();
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();


            var builder = new StringBuilder();

            if (configuration.ScriptsToInclude.JQuery)
                builder.Append(GetResource("Bifrost.Web.Scripts.jquery-1.9.1.min.js"));

            if (configuration.ScriptsToInclude.Knockout)
                builder.Append(GetResource("Bifrost.Web.Scripts.knockout-2.2.1.debug.js"));

            if (configuration.ScriptsToInclude.KnockoutMapping)
                builder.Append(GetResource("Bifrost.Web.Scripts.knockout.mapping-latest.js"));

            if (configuration.ScriptsToInclude.JQueryHistory)
                builder.Append(GetResource("Bifrost.Web.Scripts.jquery.history.js"));

            if (configuration.ScriptsToInclude.Require)
            {
                builder.Append(GetResource("Bifrost.Web.Scripts.require.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.order.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.text.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.domReady.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.noext.js"));
            }

            if (configuration.ScriptsToInclude.Bifrost)
                builder.Append(GetResource("Bifrost.Web.Scripts.Bifrost.debug.js"));

            builder.Append(proxies.All);

            var files = assetsManager.GetFilesForExtension("js");
            var serialized = JsonConvert.SerializeObject(files);
            builder.AppendFormat("Bifrost.assetsManager.initializeFromAssets({0});", serialized);
            _configurationAsString = builder.ToString();
        }
    }
}
