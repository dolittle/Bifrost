/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// A null implementation for <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    public class NullUncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        /// <inheritdoc/>
        public void Commit(TransactionCorrelationId correlationId, UncommittedEventStream eventStream)
        {
        }
    }
}