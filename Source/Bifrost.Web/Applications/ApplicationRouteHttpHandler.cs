using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

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
            var url = context.Request.Url.AbsolutePath.Replace("/"+_url,string.Empty);
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
                actualLine = actualLine.Replace("value=\"", "value=\"" + _url + "/");
            }
            else
            {

                if (line.Contains(attribute + "=") && !line.Contains(attribute + "=\"http") && !line.Contains(attribute + "='http"))
                {
                    if (line.Contains(attribute + "=\"/") && line.Contains(attribute + "='/"))
                    {
                        actualLine = actualLine.Replace(attribute + "=\"/", attribute + "=\"" + _url + "/");
                        actualLine = actualLine.Replace(attribute + "='/", attribute + "='" + _url + "/");
                    }
                    else
                    {
                        actualLine = actualLine.Replace(attribute + "=\"", attribute + "=\"" + _url + "/");
                        actualLine = actualLine.Replace(attribute + "='", attribute + "='" + _url + "/");
                    }
                }
            }
            return actualLine;
        }
    }
}
