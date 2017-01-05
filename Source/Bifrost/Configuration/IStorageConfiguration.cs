/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines a generic storage configuration
	/// </summary>
    public interface IStorageConfiguration
    {
        /// <summary>
        /// Gets or sets the configuration for storage
        /// </summary>
        IEntityContextConfiguration Storage { get; set; }
    }
}