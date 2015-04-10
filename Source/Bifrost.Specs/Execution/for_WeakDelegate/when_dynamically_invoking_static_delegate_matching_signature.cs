using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WeakDelegate
{
    public class when_dynamically_invoking_static_delegate_matching_signature
    {
        static int some_method(string stringParameter, double intParameter)
        {
            return 42;
        }

        static WeakDelegate weak_delegate;
        static object result;

        Establish context = () =>
        {
            Func<string, double, int> @delegate = some_method;
            weak_delegate = new WeakDelegate(@delegate);
        };

        Because of = () => result = weak_delegate.DynamicInvoke("Something", 42.42);

        It should_call_delegate_and_return_the_result = () => result.ShouldEqual(42);
    }
}
