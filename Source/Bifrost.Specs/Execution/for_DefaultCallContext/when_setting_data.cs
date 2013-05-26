using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_DefaultCallContext
{
    public class when_setting_data
    {
        static DefaultCallContext call_context;

        Establish context = () => call_context = new DefaultCallContext();

        Because of = () => call_context.SetData("Something", "Hello world");

        It should_return_true_if_asked_if_it_has_data = () => call_context.HasData("Something").ShouldBeTrue();
        It should_return_the_data_when_getting = () => call_context.GetData<string>("Something").ShouldEqual("Hello world");
    }
}
