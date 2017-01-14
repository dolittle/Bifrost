using Bifrost.Events.InProcess;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.InProcess.for_CommittedEventStreamSender.given
{
    public class all_dependencies 
    {
        protected static Mock<ICommittedEventStreamBridge> committed_event_stream_bridge_mock;

        Establish context = () => committed_event_stream_bridge_mock = new Mock<ICommittedEventStreamBridge>();
    }
}
