/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Interface for all configuration elements
    /// </summary>
    public interface IConfigurationElement
    {
        /// <summary>
        /// Initialization of the deriving ConfigurationElement instance
        /// </summary>
        void Initialize(IContainer container);
    }
}
