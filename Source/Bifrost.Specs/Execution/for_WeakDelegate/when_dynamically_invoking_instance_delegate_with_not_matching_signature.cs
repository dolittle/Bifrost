using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WeakDelegate
{
    public class when_dynamically_invoking_instance_delegate_with_not_matching_signature
    {
        static ClassWithMethod target;
        static WeakDelegate weak_delegate;
        static Exception exception;

        Establish context = () =>
        {
            target = new ClassWithMethod();
            Func<string, double, int> @delegate = target.SomeMethod;
            weak_delegate = new WeakDelegate(@delegate);
        };

        Because of = () => exception = Catch.Exception(() => weak_delegate.DynamicInvoke(43,"blah"));

        It should_throw_invalid_signature_exception = () => exception.ShouldBeOfExactType<InvalidSignatureException>();
    }
}
