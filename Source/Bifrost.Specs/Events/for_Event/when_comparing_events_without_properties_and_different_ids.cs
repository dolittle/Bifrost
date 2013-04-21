using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_Event
{
    public class when_comparing_events_without_properties_and_different_ids
    {
        static SimpleEvent first_event;
        static SimpleEvent second_event;
        static bool is_equal;

        Establish context = () =>
                                {
                                    first_event = new SimpleEvent(Guid.NewGuid());
                                    second_event = new SimpleEvent(Guid.NewGuid());
                                };

        Because of = () => is_equal = first_event.Equals(second_event);

        It should_be_considered_equal = () => is_equal.ShouldBeTrue();
    }
}