/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the frontend specific configuration
    /// </summary>
    public interface IFrontendConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets or sets the <see cref="IFrontendTargetConfiguration"/> to use for configuring the frontend
        /// </summary>
        IFrontendTargetConfiguration Target { get; set; }
    }
}
