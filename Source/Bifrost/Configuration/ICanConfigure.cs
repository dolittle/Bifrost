/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an interface for configuring Bifrost
    /// </summary>
    public interface ICanConfigure
    {
        /// <summary>
        /// Gets called when the application can configure Bifrost
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> to configure</param>
        void Configure(IConfigure configure);
    }
}
