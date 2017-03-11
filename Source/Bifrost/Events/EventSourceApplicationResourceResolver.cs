/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ApplicationResourceResolverFor{T}"/> that
    /// knows how to resolve <see cref="IEventSource">events</see>
    /// </summary>
    public class EventSourceApplicationResourceResolver : ApplicationResourceResolverFor<EventSourceApplicationResourceType>
    {
        /// <inheritdoc/>
        public override Type Resolve(IApplicationResourceIdentifier identifier)
        {
            throw new NotImplementedException();
        }
    }
}
