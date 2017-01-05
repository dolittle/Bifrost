/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration.Assemblies;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a default <see cref="ICanSpecifyAssemblies">assembly specifier</see> 
    /// </summary>
    public class DefaultAssemblySpecifier : ICanSpecifyAssemblies
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
#pragma warning restore 1591 // Xml Comments
    }
}
