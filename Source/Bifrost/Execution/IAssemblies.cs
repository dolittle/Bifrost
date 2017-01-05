/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a locator for locating assemblies for current application
    /// </summary>
    public interface IAssemblies
    {
        /// <summary>
        /// Gets all assemblies for current application
        /// </summary>
        /// <returns>Array of assemblies</returns>
        IEnumerable<Assembly> GetAll();

        /// <summary>
        /// Gets an assembly for the current application by its fully qualified name 
        /// </summary>
        /// <param name="fullName">Fully qualified name of the assembly</param>
        /// <returns>Instance of the assembly, null if it was not found</returns>
        Assembly GetWithFullName(string fullName);

        /// <summary>
        /// Gets an assembly based upon a friendly name of the assembly
        /// </summary>
        /// <param name="name">Name to get with</param>
        /// <returns>Instance of the assembly, null if it was not found</returns>
        Assembly GetWithName(string name);
    }
}
