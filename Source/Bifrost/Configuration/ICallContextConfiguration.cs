/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the configuration for call context
    /// </summary>
    public interface ICallContextConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets or sets the type of <see cref="ICallContext"/> to use
        /// </summary>
        Type CallContextType { get; set; }
    }
}
