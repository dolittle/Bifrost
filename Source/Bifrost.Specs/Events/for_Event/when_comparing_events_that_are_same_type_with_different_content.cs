using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_Event
{
    public class when_comparing_events_that_are_same_type_with_different_content
    {
        static SimpleEventWithOneProperty first_event;
        static SimpleEventWithOneProperty second_event;
        static bool is_equal;

        Establish context = () =>
                                {
                                    first_event = new SimpleEventWithOneProperty(Guid.NewGuid()) {SomeString = "Something"};
                                    second_event = new SimpleEventWithOneProperty(Guid.NewGuid()) {SomeString = "Something Else"};
                                };

        Because of = () => is_equal = first_event.Equals(second_event);

        It should_not_be_considered_equal = () => is_equal.ShouldBeFalse();
    }
}