using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bifrost.Applications;
using Bifrost.Events;
using Bifrost.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventEnvelopes
{
    public class when_creating_from_an_event_source_two_events_and_version : given.an_event_envelopes
    {
        static EventSourceId event_source_id;
        static Mock<IEventSource> event_source;
        static ApplicationResourceIdentifier event_source_resource_identifier;
        static EventGeneration event_generation = 42;

        static ClaimsPrincipal principal;
        static CausedBy identity_name = "Some User";
        
        static DateTime expected_time;

        static Mock<IEvent> first_event;
        static ApplicationResourceIdentifier first_event_resource_identifier;
        static EventSourceVersion first_event_version = new EventSourceVersion(4, 2);

        static Mock<IEvent> second_event;
        static ApplicationResourceIdentifier second_event_resource_identifier;
        static EventSourceVersion second_event_version = new EventSourceVersion(4, 3);

        static IEnumerable<EventAndVersion> input;
        static IEventEnvelope[] result;

        Establish context = () =>
        {
            event_source_id = Guid.NewGuid();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            event_source.SetupGet(e => e.Version).Returns(first_event_version);

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(identity.NameClaimType, identity_name));
            principal = new ClaimsPrincipal(identity);
            execution_context.SetupGet(e => e.Principal).Returns(principal);
            event_migration_hierarchy_manager.Setup(e => e.GetCurrentGenerationFor(Moq.It.IsAny<Type>())).Returns(event_generation);

            expected_time = new DateTime(2005, 3, 5, 15, 33, 0);
            system_clock.Setup(s => s.GetCurrentTime()).Returns(expected_time);

            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("Some Application");
            var segment = new Mock<IApplicationLocation>();
            segment.SetupGet(a => a.Name).Returns((BoundedContextName)"Some Context");

            first_event = new Mock<IEvent>();
            var first_event_resource = new Mock<IApplicationResource>();
            first_event_resource.SetupGet(r => r.Name).Returns("Some Resource");
            first_event_resource_identifier = new ApplicationResourceIdentifier(
                application.Object, 
                new IApplicationLocation[] { segment.Object }, 
                first_event_resource.Object);
            application_resources.Setup(a => a.Identify(first_event.Object)).Returns(first_event_resource_identifier);

            second_event = new Mock<IEvent>();
            var second_event_resource = new Mock<IApplicationResource>();
            second_event_resource.SetupGet(r => r.Name).Returns("Some Other Resource");
            second_event_resource_identifier = new ApplicationResourceIdentifier(
                application.Object,
                new IApplicationLocation[] { segment.Object },
                second_event_resource.Object);
            application_resources.Setup(a => a.Identify(second_event.Object)).Returns(second_event_resource_identifier);

            segment = new Mock<IApplicationLocation>();
            segment.SetupGet(a => a.Name).Returns((BoundedContextName)"Some Other Context");
            var resource = new Mock<IApplicationResource>();
            resource.SetupGet(r => r.Name).Returns("Some Other Resource");
            event_source_resource_identifier = new ApplicationResourceIdentifier(
                application.Object,
                new IApplicationLocation[] { segment.Object },
                resource.Object);

            application_resources.Setup(a => a.Identify(event_source.Object)).Returns(event_source_resource_identifier);

            input = new[]
            {
                new EventAndVersion(first_event.Object, first_event_version),
                new EventAndVersion(second_event.Object, second_event_version)
            };
        };

        Because of = () => result = event_envelopes.CreateFrom(event_source.Object, input).ToArray();

        It should_hold_a_not_set_correlation_id_on_the_first_event = () => result[0].CorrelationId.ShouldEqual(TransactionCorrelationId.NotSet);
        It should_hold_zero_sequence_number_on_the_first_event = () => result[0].SequenceNumber.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_zero_sequence_number_for_type_on_the_first_event = () => result[0].SequenceNumberForEventType.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_a_non_default_id_for_event_id_on_the_first_event = () => result[0].EventId.ShouldNotEqual(EventId.NotSet);
        It should_hold_the_event_source_id_on_the_first_event = () => result[0].EventSourceId.ShouldEqual(event_source_id);
        It should_hold_user_name_as_caused_by_on_the_first_event = () => result[0].CausedBy.ShouldEqual(identity_name);
        It should_hold_the_generation_on_the_first_event = () => result[0].Generation.ShouldEqual(event_generation);
        It should_hold_correct_version_on_the_first_event = () => result[0].Version.ShouldEqual(first_event_version);
        It should_hold_the_correct_occurred_time_on_the_first_event = () => result[0].Occurred.ShouldEqual(expected_time);
        It should_hold_the_event_resource_identifier_on_the_first_event = () => result[0].Event.ShouldEqual(first_event_resource_identifier);
        It should_hold_the_event_source_resource_identifier_on_the_first_event = () => result[0].EventSource.ShouldEqual(event_source_resource_identifier);

        It should_hold_a_not_set_correlation_id_on_the_second_event = () => result[1].CorrelationId.ShouldEqual(TransactionCorrelationId.NotSet);
        It should_hold_zero_sequence_number_on_the_second_event = () => result[1].SequenceNumber.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_zero_sequence_number_for_type_on_the_second_event = () => result[1].SequenceNumberForEventType.ShouldEqual(EventSequenceNumber.Zero);
        It should_hold_a_non_default_id_for_event_id_on_the_second_event = () => result[1].EventId.ShouldNotEqual(EventId.NotSet);
        It should_hold_the_event_source_id_on_the_second_event = () => result[1].EventSourceId.ShouldEqual(event_source_id);
        It should_hold_user_name_as_caused_by_on_the_second_event = () => result[1].CausedBy.ShouldEqual(identity_name);
        It should_hold_the_generation_on_the_second_event = () => result[1].Generation.ShouldEqual(event_generation);
        It should_hold_correct_version_on_the_second_event = () => result[1].Version.ShouldEqual(second_event_version);
        It should_hold_the_correct_occurred_time_on_the_second_event = () => result[1].Occurred.ShouldEqual(expected_time);
        It should_hold_the_event_resource_identifier_on_the_second_event = () => result[1].Event.ShouldEqual(second_event_resource_identifier);
        It should_hold_the_event_source_resource_identifier_on_the_second_event = () => result[1].EventSource.ShouldEqual(event_source_resource_identifier);
    }
}
