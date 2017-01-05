/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Extensions methods for <see cref="ITypeDiscoverer"/> for dealing with <see cref="ICommand"/>
    /// </summary>
    public static class TypeDiscovererExtensions
    {
        /// <summary>
        /// Get the type of the command matching the fullname.  This can be in any loaded assembly and does not require the 
        /// </summary>
        /// <param name="typeDiscoverer">instance of <see cref="ITypeDiscoverer"/> being extended</param>
        /// <param name="fullName">The full name of the type</param>
        /// <returns>the type if found, <see cref="UnknownCommandException" /> if not found or type is not a command</returns>
        public static Type GetCommandTypeByName(this ITypeDiscoverer typeDiscoverer, string fullName)
        {
            var commandType = typeDiscoverer.FindTypeByFullName(fullName);

            if(commandType == null || !commandType.HasInterface(typeof(ICommand)))
                throw new UnknownCommandException(fullName);

            return commandType;
        }
    }
}
