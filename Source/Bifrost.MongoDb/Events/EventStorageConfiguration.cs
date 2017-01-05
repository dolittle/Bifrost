/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Net;

namespace Bifrost.MongoDB.Events
{
    /// <summary>
    /// Represents the configuration for <see cref="EventStore"/>
    /// </summary>
    public class EventStorageConfiguration
    {
        /// <summary>
        /// Gets or sets the Url for the mongo server
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the default database to use
        /// </summary>
        public string DefaultDatabase { get; set; }
    }
}
