using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker
{
    public class when_invoking_with_a_float_as_a_parameter : given.a_rest_service_method_invoker_and_a_service_call_for_float_input_and_no_output
    {
        const   float expected = 42.43f;

        Establish context = () =>
            {
                parameters.Add("input", expected.ToString());
            };

        Because of = () => invoker.Invoke(base_url, service_instance, uri, parameters);

        It should_invoke_the_method_on_the_service_instance = () => service_instance.FloatInputNoOutputCalled.ShouldBeTrue();
        It should_pass_the_correct_parameter = () => service_instance.FloatInputNoOutputInput.ShouldEqual(expected);
    }
}
