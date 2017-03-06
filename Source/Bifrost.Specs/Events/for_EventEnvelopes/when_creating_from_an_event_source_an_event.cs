using System;
using System.Security.Principal;
using Bifrost.Applications;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventEnvelopes
{
    public class when_creating_from_an_event_source_an_event : given.an_event_envelopes
    {
        const string identity_name = "Some User";
        static EventGeneration event_generation = 42;
        static EventSourceVersion version = new EventSourceVersion(23, 11);

        static EventSourceId event_source_id;
        static Mock<IEventSource> event_source;
        static Mock<IEvent> @event;
        static IEventEnvelope result;
        static Mock<IPrincipal> principal;
        static Mock<IIdentity> identity;

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

            identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.Name).Returns(identity_name);
            principal = new Mock<IPrincipal>();
            principal.SetupGet(p => p.Identity).Returns(identity.Object);
            execution_context.SetupGet(e => e.Principal).Returns(principal.Object);

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

        Because of = () => result = event_envelopes.CreateFrom(event_source.Object, @event.Object);

        It should_hold_the_event_source_id = () => result.EventSourceId.ShouldEqual(event_source_id);
        It should_set_event_id_to_null = () => result.EventId.ShouldEqual(EventId.Null);
        It should_hold_user_name_as_caused_by = () => result.CausedBy.ShouldEqual(identity_name);
        It should_hold_the_generation = () => result.Generation.ShouldEqual(event_generation);
        It should_hold_correct_version = () => result.Version.ShouldEqual(version);
        It should_hold_the_correct_occurred_time = () => result.Occurred.ShouldEqual(expected_time);
        It should_hold_the_event_resource_identifier = () => result.Event.ShouldEqual(event_resource_identifier);
        It should_hold_the_event_source_resource_identifier = () => result.EventSource.ShouldEqual(event_source_resource_identifier);
    }
}
