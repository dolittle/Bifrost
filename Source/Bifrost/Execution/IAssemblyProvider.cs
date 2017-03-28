/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Collections;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that can provide <see cref="Assembly">assemblies</see>
    /// </summary>
    public interface IAssemblyProvider
    {
        /// <summary>
        /// Get all the <see cref="Assembly">assemblies</see> that can be provided
        /// </summary>
        /// <returns><see cref="IObservableCollection{Assembly}">Assemblies</see> provided</returns>
        IObservableCollection<Assembly> GetAll();
    }
}
