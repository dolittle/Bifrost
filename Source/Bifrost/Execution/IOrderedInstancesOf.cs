/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines something that can discover types and give instance of these types
    /// in a predefined order when enumerated over.
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface.</typeparam>
    /// <remarks>Enumeration will throw a <see cref="CyclicDependencyException"/> if cycles are detected.</remarks>
    public interface IOrderedInstancesOf<out T> : IEnumerable<T> where T : class
    {
    }
}
