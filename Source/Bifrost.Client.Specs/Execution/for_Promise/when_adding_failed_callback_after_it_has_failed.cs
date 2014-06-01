using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Execution.for_Promise
{
    public class when_adding_failed_callback_after_it_has_failed
    {
        static Promise promise;
        const string data = "Some Data";
        static object data_passed;

        Establish context = () => 
        {
            promise = new Promise();
            promise.Fail(data);
        };

        Because of = () => promise.Failed((p,d) => data_passed = d);

        It should_pass_the_data_to_the_callback = () => data_passed.ShouldEqual(data);
    }
}
