using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Bifrost.Events;

namespace Bifrost.Specs.Commands.for_CommandContext
{
    public class when_committing : given.a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event
    {
        static UncommittedEventStream   event_stream;

        Establish context = () => uncommitted_event_stream_coordinator.Setup(e=>e.Commit(Moq.It.IsAny<UncommittedEventStream>())).Callback((UncommittedEventStream s) => event_stream = s);

        Because of = () => command_context.Commit();

        It should_commit_on_the_uncommitted_event_stream_coordinator = () => event_stream.ShouldNotBeNull();
        It should_commit_on_the_uncommitted_event_stream_coordinator_with_the_event_in_event_stream = () => event_stream.ShouldContainOnly(uncommitted_event);
        It should_commit_aggregated_root = () => aggregated_root.CommitCalled.ShouldBeTrue();
    }
}
