using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_Event
{
    public class when_comparing_events_that_are_same_type_and_area_equal_but_with_different_ids
    {
        static SimpleEventWithOneProperty first_event;
        static SimpleEventWithOneProperty second_event;
        static bool is_equal;

        Establish context = () =>
                                {
                                    first_event = new SimpleEventWithOneProperty(Guid.NewGuid());
                                    second_event = new SimpleEventWithOneProperty(Guid.NewGuid());
                                };

        Because of = () => is_equal = first_event.Equals(second_event);

        It should_be_considered_equal = () => is_equal.ShouldBeTrue();
    }
}