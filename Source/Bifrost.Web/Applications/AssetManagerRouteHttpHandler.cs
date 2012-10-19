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
            var serialized = JsonConvert.SerializeObject(assets);
            context.Response.Write(serialized);
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
                var relativePath = file.Replace(root, string.Empty).Replace("\\","/");

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
