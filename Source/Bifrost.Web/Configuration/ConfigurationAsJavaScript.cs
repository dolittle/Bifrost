/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Bifrost.Configuration;
using Bifrost.Web.Assets;
using Bifrost.Web.Proxies;
using Newtonsoft.Json;

namespace Bifrost.Web.Configuration
{
    public class ConfigurationAsJavaScript
    {
        WebConfiguration _webConfiguration;

        string _configurationAsString;

        public ConfigurationAsJavaScript(WebConfiguration webConfiguration)
        {
            _webConfiguration = webConfiguration;
        }

        string GetResource(string name)
        {
            try
            {
                var stream = typeof(ConfigurationAsJavaScript).GetTypeInfo().Assembly.GetManifestResourceStream(name);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                var content = Encoding.UTF8.GetString(bytes);
                return content;
            }
            catch
            {
                Debug.Write($"Couldn't get the resource '{name}'");
                throw;
            }
        }

        public string AsString
        {
            get
            {
                if (string.IsNullOrEmpty(_configurationAsString)) InitializeIfNotInitialized();

                return _configurationAsString;
            }
        }

        void InitializeIfNotInitialized()
        {
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
