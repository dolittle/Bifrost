/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents the configuration for the <see cref="EventStore"/>
    /// </summary>
    public class EventStoreConfiguration
    {
        /// <summary>
        /// Path of where the <see cref="EventStore"/> will keep its files
        /// </summary>
        public string Path { get; set; }
    }
}
