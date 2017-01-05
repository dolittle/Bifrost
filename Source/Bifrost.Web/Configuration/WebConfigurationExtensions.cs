/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Web;
using Bifrost.Web.Configuration;
using Bifrost.Web.Pipeline;
using Bifrost.Web.Services;

namespace Bifrost.Configuration
{
	public static class WebConfigurationExtensions
	{
        public static IConfigure Web(this IFrontendConfiguration configuration, Action<WebConfiguration> configureCallback)
        {
            var webConfiguration = new WebConfiguration(Configure.Instance.Container.Get<NamespaceMapper>());
            Configure.Instance.Container.Bind<WebConfiguration>(webConfiguration);
            configuration.Target = webConfiguration;
            configureCallback(webConfiguration);
            return Configure.Instance;
        }

        public static WebConfiguration CacheApplicationRoute(this WebConfiguration configuration)
        {
            configuration.ApplicationRouteCached = true;
            return configuration;
        }

		public static WebConfiguration AsSinglePageApplication(this WebConfiguration configuration)
		{
			HttpModule.AddPipe(new SinglePageApplication());
			return configuration;
		}

        public static WebConfiguration WithoutJQuery(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.JQuery = false;
            return configuration;
        }

        public static WebConfiguration WithoutJQueryHistory(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.JQueryHistory = false;
            return configuration;
        }

        public static WebConfiguration WithoutKnockout(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.Knockout = false;
            return configuration;
        }

        public static WebConfiguration WithoutRequire(this WebConfiguration configuration)
        {
            configuration.ScriptsToInclude.Require = false;
            return configuration;
        }

        public static WebConfiguration Namespaces(this WebConfiguration configuration, Action<PathToNamespaceMappers> callback)
        {
            callback(configuration.PathsToNamespaces);
            return configuration;
        }
	}
}

