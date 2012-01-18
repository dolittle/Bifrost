using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventRepository
{
    [Subject(typeof (EventRepository))]
    public class when_getting_the_last_committed_version_for_an_event_source_with_events 
        : given.an_event_repository_with_persisted_events
    {
        static EventSourceVersion version;
        static EventSourceVersion last_version;
        static IEvent last_event;

        Establish context = () =>
                                {
                                    last_version = new EventSourceVersion(2, 0);
                                    last_event = new SimpleEvent(event_source_id) { Version = last_version };
                                    event_converter_mock.Setup(c => c.ToEvent(last_event_for_event_source)).Returns(last_event);
                                };

        Because of = () => version = event_repository.GetLastCommittedVersion(aggregate_root_type, event_source_id);

        It should_get_the_last_committed_version = () => version.Equals(last_version);
    }
}