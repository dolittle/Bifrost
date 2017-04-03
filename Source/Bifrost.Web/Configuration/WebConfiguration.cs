/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Bifrost.Configuration;
using Bifrost.Execution;

namespace Bifrost.Web.Configuration
{
    public class WebConfiguration : IFrontendTargetConfiguration
    {
        List<IPipe> _pipes = new List<IPipe>(); 

        public WebConfiguration(
            NamespaceMapper namespaceMapper
            )
        {
            Assets = new AssetsConfiguration();
            ScriptsToInclude = new ScriptsToInclude();
            PathsToNamespaces = new PathToNamespaceMappers();
            NamespaceMapper = namespaceMapper;

#if (NET461)
            ApplicationPhysicalPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
#else
            // Todo: Temporary hack!!! Use ContentRoot in IHostingEnvironment
            ApplicationPhysicalPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");
#endif
        }

        public AssetsConfiguration Assets { get; set; }
        public ScriptsToInclude ScriptsToInclude { get; set; }
        public PathToNamespaceMappers PathsToNamespaces { get; set; }
        public NamespaceMapper NamespaceMapper { get; set; }
        public bool ApplicationRouteCached { get; set; }

        public string ApplicationPhysicalPath { get; }
        public IEnumerable<IPipe> Pipes => _pipes;

        public void Initialize(IContainer container)
        {
        }

        public void AddPipe(IPipe pipe)
        {
            _pipes.Add(pipe);
        }
    }
}
