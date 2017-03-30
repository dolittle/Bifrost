/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Execution;

namespace Bifrost.Web.Configuration
{
    public class WebConfiguration : IFrontendTargetConfiguration
    {
        public WebConfiguration(NamespaceMapper namespaceMapper)
        {
            Assets = new AssetsConfiguration();
            ScriptsToInclude = new ScriptsToInclude();
            PathsToNamespaces = new PathToNamespaceMappers();
            NamespaceMapper = namespaceMapper;

#if(NET461)            
            ApplicationPhysicalPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
#endif
        }

        public AssetsConfiguration Assets { get; set; }
        public ScriptsToInclude ScriptsToInclude { get; set; }
        public PathToNamespaceMappers PathsToNamespaces { get; set; }
        public NamespaceMapper NamespaceMapper { get; set; }
        public bool ApplicationRouteCached { get; set; }

        public string ApplicationPhysicalPath { get; }

        public void Initialize(IContainer container)
        {
        }
    }
}
