/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanSendCommittedEventStream"/> that does nothing
    /// </summary>
    public class NullCommittedEventStreamSender : ICanSendCommittedEventStream
    {
        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
        }
    }
}
