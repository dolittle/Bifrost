/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a system that can deal with proxies for <see cref="ICommandFor{T}"/>
    /// </summary>
    public interface ICommandForProxies
    {
        /// <summary>
        /// Get <see cref="ICommandFor{T}"/> for specified command type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICommand"/> to get for</typeparam>
        /// <returns>Proxied instance of <see cref="ICommandFor{0}"/> proxy</returns>
        ICommandFor<T> GetFor<T>() where T : ICommand, new();

        /// <summary>
        /// Get <see cref="ICommandFor{T}"/> for specified command instance
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICommand"/> to get for</typeparam>
        /// <param name="instance">Instance of command to wrap in <see cref="ICommandFor{0}"/></param>
        /// <returns>Proxied instance of <see cref="ICommandFor{0}"/> proxy</returns>
        ICommandFor<T> GetFor<T>(T instance) where T : ICommand;
    }
}
