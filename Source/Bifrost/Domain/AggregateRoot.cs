/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represents the base class used for aggregated roots in your domain
    /// </summary>
    public class AggregateRoot : EventSource, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="AggregateRoot">AggregatedRoot</see>
        /// </summary>
        /// <param name="id"><see cref="EventSourceId"/> of the AggregatedRoot</param>
        /// <remarks>
        /// An <see cref="AggregateRoot"/> is a type of <see cref="IEventSource"/>
        /// </remarks>
        protected AggregateRoot(EventSourceId id) : base(id)
        {}
    }
}
