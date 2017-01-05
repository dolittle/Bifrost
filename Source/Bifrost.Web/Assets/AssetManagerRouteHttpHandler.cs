/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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

        public AssetManagerRouteHttpHandler(string url)
        {
            _url = url;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();
            IEnumerable<string> assets = new string[0];
            var extension = context.Request.Params["extension"];
            if( extension != null ) 
            {
                assets = assetsManager.GetFilesForExtension(extension);
                if (context.Request.Params["structure"] != null)
                    assets = assetsManager.GetStructureForExtension(extension);
            }
            var serialized = JsonConvert.SerializeObject(assets);
            context.Response.Write(serialized);
        }
    }
}
