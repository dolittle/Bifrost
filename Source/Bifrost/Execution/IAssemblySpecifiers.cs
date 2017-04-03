﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that knows about <see cref="ICanSpecifyAssemblies"/>
    /// </summary>
    public interface IAssemblySpecifiers
    {
        /// <summary>
        /// Specifies using specifiers found in a specific <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> to find specifiers from</param>
        void SpecifyUsingSpecifiersFrom(Assembly assembly);
    }
}
