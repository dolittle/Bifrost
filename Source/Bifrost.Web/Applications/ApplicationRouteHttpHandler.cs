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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using Bifrost.Configuration;
using Bifrost.Web.Assets;

namespace Bifrost.Web.Applications
{
    public class ApplicationRouteHttpHandler : IHttpHandler
    {
        string _url;
        Assembly _assembly;
        Dictionary<string, byte[]> _resources;

        public ApplicationRouteHttpHandler(string url, Assembly assembly)
        {
            _url = url;
            _assembly = assembly;
            var resources = _assembly.GetManifestResourceNames();
            _resources = new Dictionary<string,byte[]>();
            foreach (var resource in resources)
            {
                var stream = _assembly.GetManifestResourceStream(resource);
                var bytes = new byte[stream.Length];
                stream.Read(bytes,0,bytes.Length);

                if (resource.ToLower().Contains(".html"))
                    bytes = PrepareHtml(bytes);

                var resourceName = GetRelativePathFromResourceName(resource);
                _resources[resourceName] = bytes;
            }
        }

        string GetRelativePathFromResourceName(string resourceName)
        {
            resourceName = resourceName.Replace(_assembly.GetName().Name + ".", string.Empty);
            resourceName = resourceName.Replace("-", "_");
            resourceName = resourceName.ToLowerInvariant();
            return resourceName;
        }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            var route = "/"+_url;
            var url = context.Request.Url.AbsolutePath;
            if (url.StartsWith(route))
                url = url.Substring(route.Length);
            url.Replace("/"+_url,string.Empty);
            if (string.IsNullOrEmpty(url) || url == "/" )
                url = "index.html";

            if (url.StartsWith("/"))
                url = url.Substring(1);

            url = url.Replace("/", ".");
            url = url.Replace("-", "_");
            url = url.ToLowerInvariant();

            if (!_resources.ContainsKey(url))
            {
                context.Response.StatusCode = 404;
                return;
            }

            if (url.Contains(".png"))
                context.Response.ContentType = "image/png";
            if (url.Contains(".jpg"))
                context.Response.ContentType = "image/jpg";
            if (url.Contains(".js"))
                context.Response.ContentType = "text/javascript";
            if (url.Contains(".css"))
                context.Response.ContentType = "text/css";
            if (url.Contains(".xap"))
                context.Response.ContentType = "application/x-silverlight-app";

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);


            var bytes = _resources[url];
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
        }


        byte[] PrepareHtml(byte[] bytes)
        {
            var html = UTF8Encoding.UTF8.GetString(bytes);
            var lines = html.Split('\n');

            var actualLines = new StringBuilder();
            foreach (var line in lines)
            {
                var actualLine = line;
                if (!line.Contains("href=\"#\"") && !line.Contains("href='#'"))
                    actualLine = Replace("href", line, actualLine);

                if( line.Contains("src=") ) 
                    actualLine = Replace("src", line, actualLine);

                actualLines.Append(actualLine);
            }

            return UTF8Encoding.UTF8.GetBytes(actualLines.ToString());
        }

        string Replace(string attribute, string line, string actualLine)
        {
            if (line.Contains("<param name=\"source\" value=\""))
            {
                actualLine = actualLine.Replace("value=\"", "value=\"/" + _url + "/");
            }
            else
            {
                if (line.Contains(attribute + "=") && 
                    !line.Contains(attribute + "=\"http") && 
                    !line.Contains(attribute + "='http") &&
                    !line.Contains("signalr/hubs") )
                {
                    if (line.Contains(attribute + "=\"/") && line.Contains(attribute + "='/"))
                    {
                        actualLine = actualLine.Replace(attribute + "=\"/", attribute + "=\"/" + _url + "/");
                        actualLine = actualLine.Replace(attribute + "='/", attribute + "='/" + _url + "/");
                    }
                    else
                    {
                        actualLine = actualLine.Replace(attribute + "=\"", attribute + "=\"/" + _url + "/");
                        actualLine = actualLine.Replace(attribute + "='", attribute + "='/" + _url + "/");
                    }
                }
            }
            return actualLine;
        }
    }
}
