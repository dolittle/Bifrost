/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a utility to work with <see cref="_Assembly"/>
    /// </summary>
    public interface IAssemblyUtility
    {
        /// <summary>
        /// Check if file is an actual .NET assembly or not
        /// </summary>
        /// <param name="assemblyInfo"><see cref="AssemblyInfo"/> to check</param>
        /// <returns>True if the file is an <see cref="_Assembly"/>, false if not</returns>
        bool IsAssembly(AssemblyInfo assemblyInfo);

        /// <summary>
        /// Check if an <see cref="_Assembly"/> is dynamic
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <remarks>
        /// The need for this is questionable - the interface <see cref="_Assembly"/> does not have the IsDynamic
        /// property as the implementation <see cref="Assembly"/> has. This might go away as there has been
        /// a realization that <see cref="_Assembly"/> might not be needed, it was introduced to do testing
        /// easier. Turns out however that the implementation <see cref="Assembly"/> has pretty much everything
        /// virtual.
        /// </remarks>
        bool IsAssemblyDynamic(Assembly assembly);
    }
}
