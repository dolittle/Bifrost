/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents the configuration related to <see cref="EventProcessorStates"/>
    /// </summary>
    public class EventProcessorStatesConfiguration
    {
        /// <summary>
        /// Gets or sets the path to where information related to <see cref="EventProcessorStates"/>
        /// should be stored
        /// </summary>
        public string Path { get; set; }
    }
}
