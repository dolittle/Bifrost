/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that is capable of finding types based on base types
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Find a single implementation of a basetype
        /// </summary>
        /// <param name="types">Types to find from</param>
        /// <typeparam name="T">Basetype to find for</typeparam>
        /// <returns>Type found</returns>
        /// <remarks>
        /// If the base type is an interface, it will look for any types implementing the interface.
        /// If it is a class, it will find anyone inheriting from that class
        /// </remarks>
        /// <exception cref="ArgumentException">If there is more than one instance found</exception>
        Type FindSingle<T>(IContractToImplementorsMap types);

        /// <summary>
        /// Find multiple implementations of a basetype
        /// </summary>
        /// <param name="types">Types to find from</param>
        /// <typeparam name="T">Basetype to find for</typeparam>
        /// <returns>All types implementing or inheriting from the given basetype</returns>
        /// <remarks>
        /// If the base type is an interface, it will look for any types implementing the interface.
        /// If it is a class, it will find anyone inheriting from that class
        /// </remarks>
        IEnumerable<Type> FindMultiple<T>(IContractToImplementorsMap types);

        /// <summary>
        /// Find a single implementation of a basetype
        /// </summary>
        /// <param name="types">Types to find from</param>
        /// <param name="type">Basetype to find for</param>
        /// <returns>Type found</returns>
        /// <remarks>
        /// If the base type is an interface, it will look for any types implementing the interface.
        /// If it is a class, it will find anyone inheriting from that class
        /// </remarks>
        /// <exception cref="ArgumentException">If there is more than one instance found</exception>
        Type FindSingle(IContractToImplementorsMap types, Type type);

        /// <summary>
        /// Find multiple implementations of a basetype
        /// </summary>
        /// <param name="types">Types to find from</param>
        /// <param name="type">Basetype to find for</param>
        /// <returns>All types implementing or inheriting from the given basetype</returns>
        /// <remarks>
        /// If the base type is an interface, it will look for any types implementing the interface.
        /// If it is a class, it will find anyone inheriting from that class
        /// </remarks>
        IEnumerable<Type> FindMultiple(IContractToImplementorsMap types, Type type);

        /// <summary>
        /// Find a single type using the full name, without assembly
        /// </summary>
        /// <param name="types">Types to find from</param>
        /// <param name="fullName">full name of the type to find</param>
        /// <returns>The type is found, null otherwise</returns>
        Type FindTypeByFullName(IContractToImplementorsMap types, string fullName);
    }
}
