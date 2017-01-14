using System;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_CommittedEventStreamCoordinator
{
    public class when_handling_committed_event_stream : given.a_committed_event_stream_coordinator
    {
        static CommittedEventStream stream;

        Establish context = () => stream = new CommittedEventStream(Guid.NewGuid());

        Because of = () => committed_event_stream_coordinator.Handle(stream);

        It should_delegate_sending_to_sender = () => committed_event_stream_sender_mock.Verify(c => c.Send(stream), Times.Once);
    }
}
