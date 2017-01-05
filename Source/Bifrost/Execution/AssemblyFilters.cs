/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration.Assemblies;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyFilters"/>
    /// </summary>
    public class AssemblyFilters : IAssemblyFilters
    {
        AssembliesConfiguration _assembliesConfiguration;

        /// <summary>
        /// Initializes an instance of <see cref="AssemblyFilters"/>
        /// </summary>
        /// <param name="assembliesConfiguration"></param>
        public AssemblyFilters(AssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
        }

#pragma warning disable 1591 // Xml Comments
        public bool ShouldInclude(string filename)
        {
            return _assembliesConfiguration.Specification.IsSatisfiedBy(filename);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
