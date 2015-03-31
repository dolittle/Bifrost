using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WeakDelegate
{
    public class when_asking_if_static_delegate_is_alive
    {
        static int some_method(string stringParameter, double intParameter)
        {
            return 42;
        }

        static WeakDelegate weak_delegate;
        static bool result;

        Establish context = () =>
        {
            Func<string, double, int> @delegate = some_method;
            weak_delegate = new WeakDelegate(@delegate);
        };

        Because of = () => result = weak_delegate.IsAlive;

        It should_be_considered_alive = () => result.ShouldBeTrue();
    }
}
