using Bifrost.Commands;
using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository.given
{
    public class all_dependencies
    {
        protected static Mock<ICommandContextManager> command_context_manager;
        protected static Mock<IEventEnvelopes> event_envelopes;

        Establish context = () =>
        {
            command_context_manager = new Mock<ICommandContextManager>();
            event_envelopes = new Mock<IEventEnvelopes>();
        };
    }
}
