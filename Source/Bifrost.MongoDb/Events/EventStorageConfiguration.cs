/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Net;

namespace Bifrost.MongoDb.Events
{
    /// <summary>
    /// Represents the configuration for <see cref="EventStore"/>
    /// </summary>
    public class EventStorageConfiguration
    {
        public string Url { get; set; }
        public bool UseSSL { get; set; }
        public string DefaultDatabase { get; set; }
    }
}
