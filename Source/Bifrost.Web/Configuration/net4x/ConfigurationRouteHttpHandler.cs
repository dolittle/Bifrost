/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Configuration
{
    public class ConfigurationRouteHttpHandler : IHttpHandler
    {
        WebConfiguration _webConfiguration;
        ConfigurationAsJavaScript _configurationAsJavaScript;

        public ConfigurationRouteHttpHandler() : 
            this(
                Configure.Instance.Container.Get<WebConfiguration>(),
                Configure.Instance.Container.Get<ConfigurationAsJavaScript>())
        {
        }

        public ConfigurationRouteHttpHandler(WebConfiguration webConfiguration, ConfigurationAsJavaScript configurationAsJavaScript)
        {
            _webConfiguration = webConfiguration;
            _configurationAsJavaScript = configurationAsJavaScript;
        }

        public bool IsReusable => true;


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
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_configurationAsJavaScript.AsString);
        }
    }
}
