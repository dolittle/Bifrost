using System;
using System.Reflection;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(typeof (EventSourceExtensions))]
    public class when_getting_the_on_method_on_an_event_source_for_an_event : given.two_different_event_source_types_that_handle_different_events
    {
        protected static MethodInfo handled_event_for_first_event_source;
        protected static MethodInfo unhandled_event_for_first_event_source;
        protected static MethodInfo handled_event_for_second_event_source;
        protected static MethodInfo unhandled_event_for_second_event_source;
        protected static MethodInfo second_handled_event_for_second_event_source;

        Because of = () =>
                         {
                             handled_event_for_first_event_source = event_source.GetOnMethod(simple_event);
                             //unhandled_event_for_first_event_source = event_source.GetHandleMethod(another_simple_event);

                             handled_event_for_second_event_source = second_event_source.GetOnMethod(another_simple_event);
                             //unhandled_event_for_second_event_source = second_event_source.GetHandleMethod(simple_event);

                             second_handled_event_for_second_event_source = second_event_source.GetOnMethod(new SimpleEventWithOneProperty(second_event_source_id));
                         };

        It should_get_the_correct_handle_method_for_the_simple_event = () => handled_event_for_first_event_source.ShouldNotBeNull();
        It should_get_the_correct_handle_method_for_another_simple_event = () => handled_event_for_second_event_source.ShouldNotBeNull();
        It should_get_the_correct_handle_method_for_event_with_pne_property = () => handled_event_for_second_event_source.ShouldNotBeNull();
    }
}