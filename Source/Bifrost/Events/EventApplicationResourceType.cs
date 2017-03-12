/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IApplicationResourceType">application resource type</see> for 
    /// <see cref="IEvent">events</see>
    /// </summary>
    public class EventApplicationResourceType : IApplicationResourceType
    {
        /// <inheritdoc/>
        public string Identifier => "Event";

        /// <inheritdoc/>
        public Type Type => typeof(IEvent);

        /// <inheritdoc/>
        public ApplicationArea Area => ApplicationAreas.Events;
    }
}
