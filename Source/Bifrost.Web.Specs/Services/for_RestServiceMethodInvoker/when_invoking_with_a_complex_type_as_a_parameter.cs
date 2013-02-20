using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_with_a_complex_type_as_a_parameter : given.a_rest_service_method_invoker_and_a_service_call_for_complex_input_and_no_output
    {
        const   int  expected_int = 42;
        const   string expected_string = "something";
        const   double expected_double = 43.5d;
        static string json;
        static ComplexType expected_parameter;

        Establish context = () =>
            {
                json = string.Format("{{ IntValue : {0}, StringValue : {1}, DoubleValue : {2}}}",
                                expected_int, expected_string, expected_double);
                parameters.Add("input", json);
                expected_parameter = new ComplexType {
                    IntValue = expected_int,
                    StringValue = expected_string,
                    DoubleValue = expected_double
                };
                serializer_mock.Setup(s => s.FromJson(typeof(ComplexType), json, null)).Returns(expected_parameter);
            };

        Because of = () => invoker.Invoke(base_url, service_instance, uri, parameters);

        It should_invoke_the_method_on_the_service_instance = () => service_instance.ComplexInputNoOutputCalled.ShouldBeTrue();
        It should_pass_the_correct_parameter = () => service_instance.ComplexInputNoOutputResult.ShouldEqual(expected_parameter);
    }
}
