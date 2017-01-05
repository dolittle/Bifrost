/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Represents the configuration for the <see cref="EventStore"/>
    /// </summary>
    public class EventStorageConfiguration
    {
        /// <summary>
        /// Gets or sets the url endpoint for the database server
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the database id 
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the authorization key
        /// </summary>
        public string AuthorizationKey { get; set; }
    }
}
