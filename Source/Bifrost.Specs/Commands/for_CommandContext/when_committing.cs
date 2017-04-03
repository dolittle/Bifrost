﻿using Bifrost.Events;
using Bifrost.Lifecycle;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContext
{
    public class when_committing : given.a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event
    {
        static UncommittedEventStream   event_stream;

        Establish context = () => uncommitted_event_stream_coordinator.Setup(e=>e.Commit(command_context.TransactionCorrelationId,Moq.It.IsAny<UncommittedEventStream>())).Callback((TransactionCorrelationId i, UncommittedEventStream s) => event_stream = s);

        Because of = () => command_context.Commit();

        It should_commit_on_the_uncommitted_event_stream_coordinator = () => event_stream.ShouldNotBeNull();
        It should_commit_on_the_uncommitted_event_stream_coordinator_with_the_event_in_event_stream = () => event_stream.ShouldContainOnly(uncommitted_event);
        It should_commit_aggregated_root = () => aggregated_root.CommitCalled.ShouldBeTrue();
    }
}
