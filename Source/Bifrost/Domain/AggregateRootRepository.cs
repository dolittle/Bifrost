/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
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

		/// <summary>
		/// Initializes a new instance of <see cref="AggregateRootRepository{T}">AggregatedRootRepository</see>
		/// </summary>
		/// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking </param>
		public AggregateRootRepository(ICommandContextManager commandContextManager)
		{
			_commandContextManager = commandContextManager;
		}

        /// <inheritdoc/>
		public T Get(EventSourceId id)
        {
            var commandContext = _commandContextManager.GetCurrent();
            var type = typeof(T);
            var constructor = GetConstructorFor(type);
            ThrowIfConstructorIsInvalid(type, constructor);

            var aggregateRoot = GetInstanceFrom(id, constructor);
            if (null != aggregateRoot)
            {
                if (!aggregateRoot.IsStateless())
                    ReApplyEvents(commandContext, aggregateRoot);
                else
                    FastForward(commandContext, aggregateRoot);
            }
            commandContext.RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        void FastForward(ICommandContext commandContext, T aggregateRoot)
        {
            var version = commandContext.GetLastCommittedVersionFor(aggregateRoot);
            aggregateRoot.FastForward(version);
        }

        void ReApplyEvents(ICommandContext commandContext, T aggregateRoot)
        {
            var stream = commandContext.GetCommittedEventsFor(aggregateRoot);
            if (stream.HasEvents)
                aggregateRoot.ReApply(stream);
        }

        T GetInstanceFrom(EventSourceId id, System.Reflection.ConstructorInfo constructor)
        {
            return (constructor.GetParameters()[0].ParameterType == typeof(EventSourceId) ?
                constructor.Invoke(new object[] { id }) :
                constructor.Invoke(new object[] { id.Value })) as T;
        }

        System.Reflection.ConstructorInfo GetConstructorFor(Type type)
        {
            return type.GetConstructors().Where(c =>
            {
                var parameters = c.GetParameters();
                return parameters.Length == 1 &&
                    (parameters[0].ParameterType == typeof(Guid) ||
                    parameters[0].ParameterType == typeof(EventSourceId));
            }).SingleOrDefault();
        }

        void ThrowIfConstructorIsInvalid(Type type, System.Reflection.ConstructorInfo constructor)
        {
            if (constructor == null) throw new InvalidAggregateRootConstructorSignature(type);
        }
    }
}