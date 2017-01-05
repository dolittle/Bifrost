/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents the configuration for <see cref="ICallContext"/>
    /// </summary>
    public class CallContextConfiguration : ICallContextConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CallContextConfiguration"/>
        /// </summary>
        public CallContextConfiguration()
        {
            CallContextType = typeof(DefaultCallContext);
        }

#pragma warning disable 1591 // Xml Comments
        public Type CallContextType { get; set; }

        public void Initialize(IContainer container)
        {
            container.Bind<ICallContext>(CallContextType);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
