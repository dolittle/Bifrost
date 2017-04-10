using System;
using System.Security.Claims;
using System.Security.Principal;
using Bifrost.Applications;
using Bifrost.Events;
using Bifrost.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventEnvelopes
{
    public class when_creating_from_an_event_source_an_event : given.an_event_envelopes
    {
        static CausedBy identity_name = "Some User";
        static EventGeneration event_generation = 42;
        static EventSourceVersion version = new EventSourceVersion(23, 11);

        static EventSourceId event_source_id;
        static Mock<IEventSource> event_source;
        static Mock<IEvent> @event;
        static IEventEnvelope result;
        static ClaimsPrincipal principal;

        static DateTime expected_time;

        static ApplicationResourceIdentifier event_resource_identifier;
        static ApplicationResourceIdentifier event_source_resource_identifier;

        Establish context = () =>
        {
            event_source_id = Guid.NewGuid();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            event_source.SetupGet(e => e.Version).Returns(version);
            @event = new Mock<IEvent>();

            principal = new ClaimsPrincipal(new ClaimsIdentity());
            execution_context.SetupGet(e => e.Principal).Returns(principal);

            event_migration_hierarchy_manager.Setup(e => e.GetCurrentGenerationFor(@event.Object.GetType())).Returns(event_generation);

            expected_time = new DateTime(2005, 3, 5, 15, 33, 0);
            system_clock.Setup(s => s.GetCurrentTime()).Returns(expected_time);

            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("Some Application");
            var segment = new Mock<IApplicationLocation>();
            segment.SetupGet(a => a.Name).Returns((BoundedContextName)"Some Context");
            var resource = new Mock<IApplicationResource>();
            resource.SetupGet(r => r.Name).Returns("Some Resource");

            event_resource_identifier = new ApplicationResourceIdentifier(
                application.Object, 
                new IApplicationLocation[] { segment.Object }, 
                resource.Object);

            application_resources.Setup(a => a.Identify(@event.Object)).Returns(event_resource_identifier);

            segment = new Mock<IApplicationLocation>();
            segment.SetupGet(a => a.Name).Returns((BoundedContextName)"Some Other Context");
            resource = new Mock<IApplicationResource>();
            resource.SetupGet(r => r.Name).Returns("Some Other Resource");
            event_source_resource_identifier = new ApplicationResourceIdentifier(
                application.Object,
                new IApplicationLocation[] { segment.Object },
                resource.Object);

            application_resources.Setup(a => a.Identify(event_source.Object)).Returns(event_source_resource_identifier);
        };

        Because of = () => result = event_envelopes.CreateFrom(event_source.Object, @event.Object, version);

        It should_hold_a_not_set_correlation_id = () => result.CorrelationId.ShouldEqual(TransactionCorrelationId.NotSet);
        It should_hold_zero_sequence_number = () => result.SequenceNumber.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_zero_sequence_number_for_type = () => result.SequenceNumberForEventType.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_a_non_default_id_for_event_id = () => result.EventId.ShouldNotEqual(EventId.NotSet);
        It should_hold_the_event_source_id = () => result.EventSourceId.ShouldEqual(event_source_id);
        It should_hold_user_name_as_caused_by = () => result.CausedBy.ShouldEqual(identity_name);
        It should_hold_the_generation = () => result.Generation.ShouldEqual(event_generation);
        It should_hold_correct_version = () => result.Version.ShouldEqual(version);
        It should_hold_the_correct_occurred_time = () => result.Occurred.ShouldEqual(expected_time);
        It should_hold_the_event_resource_identifier = () => result.Event.ShouldEqual(event_resource_identifier);
        It should_hold_the_event_source_resource_identifier = () => result.EventSource.ShouldEqual(event_source_resource_identifier);
    }
}
