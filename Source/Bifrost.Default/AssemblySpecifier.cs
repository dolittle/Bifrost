/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Bifrost.Configuration.Assemblies;
using Bifrost.Execution;

namespace Web
{
    public class AssemblySpecifier : ICanSpecifyAssemblies
    {
        public void Specify(IAssembliesConfiguration configuration)
        {
            configuration.IncludeAssembliesStartingWith("Web.dll");
        }
    }
}
