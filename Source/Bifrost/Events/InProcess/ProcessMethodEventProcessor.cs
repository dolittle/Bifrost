/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Applications;
using Bifrost.Execution;
using Bifrost.Time;
using Bifrost.Logging;

namespace Bifrost.Events.InProcess
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessor"/> for systems marked with the
    /// <see cref="IProcessEvents"/> marker interface and has the "Process" method according to the
    /// convention
    /// </summary>
    public class ProcessMethodEventProcessor : IEventProcessor
    {
        IContainer _container;
        MethodInfo _methodInfo;
        ISystemClock _systemClock;
        ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessor"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to use for getting instances of <see cref="IProcessEvents"/> implementation</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for timing purposes</param>
        /// <param name="identifier"><see cref="EventProcessorIdentifier"/> that uniquely identifies the <see cref="ProcessMethodEventProcessor"/></param>
        /// <param name="event"><see cref="IApplicationResourceIdentifier">Identifier</see> for identifying the <see cref="IEvent"/></param>
        /// <param name="methodInfo"><see cref="MethodInfo"/> for the actual process method</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public ProcessMethodEventProcessor(
            IContainer container,
            ISystemClock systemClock,
            EventProcessorIdentifier identifier,
            IApplicationResourceIdentifier @event,
            MethodInfo methodInfo,
            ILogger logger)
        {
            Identifier = identifier;
            Event = @event;

            _container = container;
            _systemClock = systemClock;
            _methodInfo = methodInfo;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier Event { get; }

        /// <inheritdoc/>
        public EventProcessorIdentifier Identifier { get; }

        /// <inheritdoc/>
        public IEventProcessingResult Process(IEventEnvelope envelope, IEvent @event)
        {
            var status = EventProcessingStatus.Success;
            var start = _systemClock.GetCurrentTime();
            var messages = new EventProcessingMessage[0];

            _logger.Information("Process event");

            try
            {
                var processor = _container.Get(_methodInfo.DeclaringType);
                _methodInfo.Invoke(processor, new[] { @event });
            } catch (Exception ex)
            {
                _logger.Critical(ex, "Failed during processing");
                status = EventProcessingStatus.Failed;
                messages = new[] {
                    new EventProcessingMessage(EventProcessingMessageSeverity.Error, ex.Message, ex.StackTrace.Split(Environment.NewLine.ToCharArray()))
                };
            }
            var end = _systemClock.GetCurrentTime();

            return new EventProcessingResult(envelope.CorrelationId, this, status, start, end, messages);
        }
    }
}
