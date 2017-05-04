/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Logging;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamCoordinator"/>
    /// </summary>
    [Singleton]
    public class CommittedEventStreamCoordinator : ICommittedEventStreamCoordinator
    {
        ICanReceiveCommittedEventStream _committedEventStreamReceiver;
        IEventProcessors _eventProcessors;
        IEventProcessorLog _eventProcessorLog;
        IEventProcessorStates _eventProcessorStates;
        ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="committedEventStreamReceiver"><see cref="ICanReceiveCommittedEventStream">Committed event stream receiver</see> for receiving events</param>
        /// <param name="eventProcessors"></param>
        /// <param name="eventProcessorLog"></param>
        /// <param name="eventProcessorStates"></param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging</param>
        public CommittedEventStreamCoordinator(
            ICanReceiveCommittedEventStream committedEventStreamReceiver,
            IEventProcessors eventProcessors,
            IEventProcessorLog eventProcessorLog,
            IEventProcessorStates eventProcessorStates,
            ILogger logger)
        {
            _committedEventStreamReceiver = committedEventStreamReceiver;
            _eventProcessors = eventProcessors;
            _eventProcessorLog = eventProcessorLog;
            _eventProcessorStates = eventProcessorStates;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _committedEventStreamReceiver.Received += CommittedEventStreamReceived;
        }

        void CommittedEventStreamReceived(CommittedEventStream committedEventStream)
        {
            _logger.Information($"Committing event stream with {committedEventStream.Count} events");
            committedEventStream.ForEach(e =>
            {
                var results = _eventProcessors.Process(e.Envelope, e.Event);
                Parallel.ForEach(results, result =>
                {
                    if (result.Status == EventProcessingStatus.Success)
                    {
                        _logger.Information("Events processed successfully");
                        _eventProcessorStates.ReportSuccessFor(result.EventProcessor, e.Event, e.Envelope);

                        if( result.Messages.Count() > 0 )
                        {
                            _eventProcessorLog.Info(result.EventProcessor, e.Event, e.Envelope, result.Messages);
                        }
                    }
                    else
                    {
                        _logger.Error($"Problems processing with {result.EventProcessor.Identifier}");

                        _eventProcessorStates.ReportFailureFor(result.EventProcessor, e.Event, e.Envelope);
                        _eventProcessorLog.Failed(result.EventProcessor, e.Event, e.Envelope, result.Messages);
                    }
                });
            });
        }
    }
}
