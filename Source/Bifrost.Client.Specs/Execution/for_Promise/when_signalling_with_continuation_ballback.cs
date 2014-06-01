using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Execution.for_Promise
{
    public class when_signalling_with_continuation_ballback
    {
        static Promise promise;
        const string data = "Some Data";
        static object data_passed;

        Establish context = () => 
        {
            promise = new Promise();
            promise.ContinueWith((p,d) => data_passed = d);
        };

        Because of = () => promise.Signal(data);

        It should_pass_the_data_to_the_callback = () => data_passed.ShouldEqual(data);
    }
}
