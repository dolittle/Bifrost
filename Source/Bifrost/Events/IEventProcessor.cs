﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Application;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines something that is capable of processing an event 
    /// </summary>
    public interface IEventProcessor
    {
        /// <summary>
        /// Gets the <see cref="ApplicationResourceIdentifierFor"/> for the <see cref="IEventProcessor"/>
        /// </summary>
        ApplicationResourceIdentifierFor<IEvent>   Identifier { get; } 

        /// <summary>
        /// Process an event 
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        /// <returns><see cref="EventProcessorResult">Result</see> from the processing</returns>
        IEventProcessingResult Process(IEvent @event);
    }
}