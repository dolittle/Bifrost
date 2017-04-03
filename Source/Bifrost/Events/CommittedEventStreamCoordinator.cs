/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Bifrost.Execution;
using Bifrost.Extensions;

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

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="committedEventStreamReceiver"><see cref="ICanReceiveCommittedEventStream">Committed event stream receiver</see> for receiving events</param>
        /// <param name="eventProcessors"></param>
        /// <param name="eventProcessorLog"></param>
        /// <param name="eventProcessorStates"></param>
        public CommittedEventStreamCoordinator(
            ICanReceiveCommittedEventStream committedEventStreamReceiver,
            IEventProcessors eventProcessors,
            IEventProcessorLog eventProcessorLog,
            IEventProcessorStates eventProcessorStates)
        {
            _committedEventStreamReceiver = committedEventStreamReceiver;
            _eventProcessors = eventProcessors;
            _eventProcessorLog = eventProcessorLog;
            _eventProcessorStates = eventProcessorStates;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _committedEventStreamReceiver.Received += CommittedEventStreamReceived;
        }

        void CommittedEventStreamReceived(CommittedEventStream committedEventStream)
        {
            committedEventStream.ForEach(e =>
            {
                var results = _eventProcessors.Process(e.Envelope, e.Event);
                Parallel.ForEach(results, result =>
                {
                    if (result.Status == EventProcessingStatus.Success)
                    {
                        _eventProcessorStates.ReportSuccessFor(result.EventProcessor, e.Event, e.Envelope);

                        if( result.Messages.Count() > 0 )
                        {
                            _eventProcessorLog.Info(result.EventProcessor, e.Event, e.Envelope, result.Messages);
                        }
                    }
                    else
                    {
                        _eventProcessorStates.ReportFailureFor(result.EventProcessor, e.Event, e.Envelope);
                        _eventProcessorLog.Failed(result.EventProcessor, e.Event, e.Envelope, result.Messages);
                    }
                });
            });
        }
    }
}
