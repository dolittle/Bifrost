using System;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStoreChangeManager
{
    public class when_registering_a_notifier_that_does_not_implement_the_notifier_interface : given.an_event_store_change_notifier
    {
        static Exception thrown_exception;

        Because of = () => thrown_exception = Catch.Exception(() => event_store_change_manager.Register(typeof(string)));

        It should_throw_argument_exception = () => thrown_exception.ShouldBeOfType<ArgumentException>();
    }
}
