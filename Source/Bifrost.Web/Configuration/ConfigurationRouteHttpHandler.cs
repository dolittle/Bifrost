/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
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

        WebConfiguration _webConfiguration;

        public ConfigurationRouteHttpHandler() : this(Configure.Instance.Container.Get<WebConfiguration>())
        {
        }

        public ConfigurationRouteHttpHandler(WebConfiguration webConfiguration)
        {
            _webConfiguration = webConfiguration;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (_webConfiguration.ApplicationRouteCached)
            {
                if (string.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
                {
                    context.Response.Cache.SetAllowResponseInBrowserHistory(true);
                    context.Response.Cache.SetExpires(DateTime.Now.AddYears(10));
                    context.Response.Cache.SetCacheability(HttpCacheability.Private);

                    context.Response.Cache.VaryByHeaders["If-Modified-Since"] = true;
                    context.Response.Cache.VaryByHeaders["If-None-Match"] = true;
                    context.Response.Cache.SetETag(DateTime.Now.ToString());

                    context.Response.Cache.SetValidUntilExpires(true);

                    context.Response.Cache.SetNoServerCaching();
                    context.Response.Cache.SetLastModified(DateTime.MinValue);
                    OutputContent(context);
                }
                else
                {
                    context.Response.StatusCode = 304;
                    context.Response.StatusDescription = "Not Modified";
                }
            }
            else
            {
                OutputContent(context);
            }
        }

        void OutputContent(HttpContext context)
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
            
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();

            var builder = new StringBuilder();

            if (_webConfiguration.ScriptsToInclude.JQuery)
                builder.Append(GetResource("Bifrost.Web.Scripts.jquery-2.1.3.min.js"));

            if (_webConfiguration.ScriptsToInclude.Knockout)
                builder.Append(GetResource("Bifrost.Web.Scripts.knockout-3.2.0.debug.js"));

            if (_webConfiguration.ScriptsToInclude.SignalR)
                builder.Append(GetResource("Bifrost.Web.Scripts.jquery.signalR-2.2.0.js"));

            if (_webConfiguration.ScriptsToInclude.JQueryHistory)
                builder.Append(GetResource("Bifrost.Web.Scripts.jquery.history.js"));

            if (_webConfiguration.ScriptsToInclude.Require)
            {
                builder.Append(GetResource("Bifrost.Web.Scripts.require.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.order.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.text.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.domReady.js"));
                builder.Append(GetResource("Bifrost.Web.Scripts.noext.js"));
            }

            if (_webConfiguration.ScriptsToInclude.Bifrost)
                builder.Append(GetResource("Bifrost.Web.Scripts.Bifrost.debug.js"));

            builder.Append(proxies.All);

            var files = assetsManager.GetFilesForExtension("js");
            var serialized = JsonConvert.SerializeObject(files);
            builder.AppendFormat("Bifrost.assetsManager.initializeFromAssets({0});", serialized);
            _configurationAsString = builder.ToString();
        }
    }
}
