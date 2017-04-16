/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSourceVersions"/> for holding <see cref="EventSourceVersion"/>
    /// for each <see cref="EventSource"/> on the filesystem
    /// </summary>
    public class EventSourceVersions : IEventSourceVersions
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersions"/>
        /// </summary>
        /// <param name="files">A system to work with <see cref="IFiles"/></param>
        /// <param name="pathProvider">A delegate that can provide path to store <see cref="EventSourceVersion"/> for <see cref="IEventSource"/> - see <see cref="ICanProvideEventSourceVersionsPath"/></param>
        public EventSourceVersions(IFiles files, ICanProvideEventSourceVersionsPath pathProvider)
        {

        }

        /// <inheritdoc/>
        public EventSourceVersion GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void SetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId, EventSourceVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
