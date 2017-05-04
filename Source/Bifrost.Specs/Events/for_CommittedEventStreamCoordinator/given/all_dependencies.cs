using Bifrost.Events;
using Bifrost.Logging;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_CommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ICanReceiveCommittedEventStream> committed_event_stream_receiver_mock;
        protected static Mock<IEventProcessors> event_processors;
        protected static Mock<IEventProcessorLog> event_processor_log;
        protected static Mock<IEventProcessorStates> event_processor_states;
        protected static ILogger logger;

        Establish context = () =>
        {
            committed_event_stream_receiver_mock = new Mock<ICanReceiveCommittedEventStream>();
            event_processors = new Mock<IEventProcessors>();
            event_processor_log = new Mock<IEventProcessorLog>();
            event_processor_states = new Mock<IEventProcessorStates>();
            logger = Mock.Of<ILogger>();
        };
    }
}
