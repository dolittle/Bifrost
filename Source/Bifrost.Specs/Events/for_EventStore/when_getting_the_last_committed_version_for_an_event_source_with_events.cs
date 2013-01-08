using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;
using Bifrost.Fakes.Domain;

namespace Bifrost.Specs.Events.for_EventStore
{
    [Subject(typeof (EventStore))]
    public class when_getting_the_last_committed_version_for_an_event_source_with_events : given.an_event_store_with_persisted_events
    {
        static EventSourceVersion version;
        static EventSourceVersion last_version;
        static IEvent last_event;

        Establish context = () =>
                                {
                                    last_version = new EventSourceVersion(2, 0);
                                    last_event = new SimpleEvent(event_source_id) { Version = last_version };
                                };

        Because of = () => version = event_store.GetLastCommittedVersion(new StatefulAggregatedRoot(event_source_id), event_source_id);

        It should_get_the_last_committed_version = () => version.Equals(last_version);
    }
}