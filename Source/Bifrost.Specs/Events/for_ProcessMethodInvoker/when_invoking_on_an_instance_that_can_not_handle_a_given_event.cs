using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_ProcessMethodInvoker
{
    public class when_invoking_on_an_instance_that_can_not_handle_a_given_event : given.a_process_method_invoker
    {
        static object instance;
        static SimpleEvent @event;
        static bool result;

        Establish context = () =>
                                {
                                    instance = new object();
                                    @event = new SimpleEvent(Guid.NewGuid());
                                };


        Because of = () => result = invoker.TryProcess(instance, @event);

        It should_result_in_false = () => result.ShouldBeFalse();
    }
}