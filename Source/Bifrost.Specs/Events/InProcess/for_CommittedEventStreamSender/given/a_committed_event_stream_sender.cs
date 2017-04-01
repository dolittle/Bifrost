using Bifrost.Events.InProcess;
using Machine.Specifications;

namespace Bifrost.Specs.Events.InProcess.for_CommittedEventStreamSender.given
{
    public class a_committed_event_stream_sender : all_dependencies
    {
        protected static CommittedEventStreamSender committed_event_stream_sender;

        Establish context = () => committed_event_stream_sender = new CommittedEventStreamSender(committed_event_stream_bridge_mock.Object);
    }
}
