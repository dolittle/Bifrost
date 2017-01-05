/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that handles the relationship between contracts and their implementors
    /// </summary>
    /// <remarks>
    /// A contract is considered an abstract type or an interface
    /// </remarks>
    public interface IContractToImplementorsMap
    {
        /// <summary>
        /// Feed the map with types
        /// </summary>
        /// <param name="types"><see cref="IEnumerable{Type}">Types</see> to feed with</param>
        void Feed(IEnumerable<Type> types);

        /// <summary>
        /// Gets all the types in the map
        /// </summary>
        IEnumerable<Type> All { get; }

        /// <summary>
        /// Retrieve implementors of a specific contract
        /// </summary>
        /// <typeparam name="T">Type of contract to retrieve for</typeparam>
        /// <returns><see cref="IEnumerable{T}">Types</see> implementing the contract</returns>
        IEnumerable<Type> GetImplementorsFor<T>();
        /// <summary>
        /// Retrieve implementors of a specific contract
        /// </summary>
        /// <param name="contract"><see cref="Type"/> of contract to retrieve for</param>
        /// <returns><see cref="IEnumerable{T}">Types</see> implementing the contract</returns>
        IEnumerable<Type> GetImplementorsFor(Type contract);
    }
}
