/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Commands;
using Bifrost.Events;

namespace Bifrost.Domain
{
	/// <summary>
	/// Defines a concrete implementation of <see cref="IAggregateRootRepository{T}">IAggregatedRootRepository</see>
	/// </summary>
	/// <typeparam name="T">Type the repository is for</typeparam>
	public class AggregateRootRepository<T> : IAggregateRootRepository<T>
		where T : AggregateRoot
	{
		ICommandContextManager _commandContextManager;
        IEventEnvelopes _eventEnvelopes;

		/// <summary>
		/// Initializes a new instance of <see cref="AggregateRootRepository{T}">AggregatedRootRepository</see>
		/// </summary>
		/// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking </param>
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> to use for getting the envelope for the <see cref="IEvent"/> when they are applied</param>
		public AggregateRootRepository(ICommandContextManager commandContextManager, IEventEnvelopes eventEnvelopes)
		{
			_commandContextManager = commandContextManager;
            _eventEnvelopes = eventEnvelopes;
		}

        /// <inheritdoc/>
		public T Get(EventSourceId id)
		{
			var commandContext = _commandContextManager.GetCurrent();
			var type = typeof (T);
			var aggregateRoot = Activator.CreateInstance(type, id) as T;
            aggregateRoot.EventEnvelopes = _eventEnvelopes;
			if (null != aggregateRoot)
			{
                if(!aggregateRoot.IsStateless())
                {
                    var stream = commandContext.GetCommittedEventsFor(aggregateRoot);
                    if( stream.HasEvents )
                        aggregateRoot.ReApply(stream);
                }
                else
                {
                    var version = commandContext.GetLastCommittedVersionFor(aggregateRoot);
                    aggregateRoot.FastForward(version);
                }
			}
            commandContext.RegisterForTracking(aggregateRoot);

			return aggregateRoot;
		}
	}
}