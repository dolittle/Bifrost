using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Bifrost.Web.Applications
{
    public class ApplicationRouteHttpHandler : IHttpHandler
    {
        string _url;
        Assembly _assembly;
        Dictionary<string, Stream> _resources;

        public ApplicationRouteHttpHandler(string url, Assembly assembly)
        {
            _url = url;
            _assembly = assembly;
            var resources = _assembly.GetManifestResourceNames();
            _resources = new Dictionary<string,Stream>();
            foreach (var resource in resources)
                _resources[GetRelativePathFromResourceName(resource)] = _assembly.GetManifestResourceStream(resource);

        }

        string GetRelativePathFromResourceName(string resourceName)
        {
            resourceName = resourceName.Replace(_assembly.GetName().Name + ".", string.Empty);
            resourceName = resourceName.Replace(".", "/");
            var indexOfSlash = resourceName.LastIndexOf("/");
            var actualResourceName = string.Format("{0}.{1}",
                resourceName.Substring(0, indexOfSlash),
                resourceName.Substring(indexOfSlash+1)
                );
            return actualResourceName;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            var url = context.Request.Url.AbsolutePath.Replace("/"+_url,string.Empty);
            if (string.IsNullOrEmpty(url))
                url = "index.html";

            if (!_resources.ContainsKey(url))
                return;

            var resource = _resources[url];
            var bytes = new byte[resource.Length];
            resource.Read(bytes,0,bytes.Length);
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
    }
}
