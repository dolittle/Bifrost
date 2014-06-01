using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Execution.for_Promise
{
    public class when_failing_with_ballback
    {
        static Promise promise;
        const string data = "Some Data";
        static object data_passed;

        Establish context = () => 
        {
            promise = new Promise();
            promise.Failed((p,d) => data_passed = d);
        };

        Because of = () => promise.Fail(data);

        It should_pass_the_data_to_the_callback = () => data_passed.ShouldEqual(data);
    }
}
