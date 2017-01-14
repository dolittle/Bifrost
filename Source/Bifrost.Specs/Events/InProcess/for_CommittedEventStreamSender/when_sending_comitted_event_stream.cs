using System;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.InProcess.for_CommittedEventStreamSender
{
    public class when_sending_comitted_event_stream : given.a_committed_event_stream_sender
    {
        protected static CommittedEventStream committed_event_stream;

        Establish context = () => committed_event_stream = new CommittedEventStream(Guid.NewGuid());

        Because of = () => committed_event_stream_sender.Send(committed_event_stream);

        It should_forward_to_the_bridge = () => committed_event_stream_bridge_mock.Verify(c => c.Send(committed_event_stream), Times.Once());
    }
}
