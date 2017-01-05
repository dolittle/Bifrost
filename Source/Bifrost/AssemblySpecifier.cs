/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration.Assemblies;
using Bifrost.Execution;

namespace Bifrost
{
    /// <summary>
    /// Reperesents an <see cref="ICanSpecifyAssemblies">assembly specifier</see> for client aspects
    /// </summary>
    public class AssemblySpecifier : ICanSpecifyAssemblies
    {
#pragma warning disable 1591 // Xml Comments
        public void Specify(IAssemblyRuleBuilder builder)
        {
            builder.ExcludeAssembliesStartingWith(
                "System",
                "mscorlib",
                "Microsoft"
            );
        }
#pragma warning disable 1591 // Xml Comments
    }
}
