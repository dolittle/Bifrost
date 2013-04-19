using System;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Bifrost.Serialization;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_a_method_without_parameters_but_returns_complex_type : given.a_rest_service_method_invoker_and_a_service_call
    {
        const int expected_int = 42;
        const string expected_string = "something";
        const double expected_double = 43.5d;
        static string json;
        static ComplexType expected_result;
        static string result;

        Establish context = () =>
        {
            uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.NoInputComplexOutputMethod));

            
            json = string.Format("{{ IntValue : {0}, StringValue : {1}, DoubleValue : {2}}}",
                            expected_int, expected_string, expected_double);
            
            expected_result = new ComplexType
            {
                IntValue = expected_int,
                StringValue = expected_string,
                DoubleValue = expected_double
            };
            service_instance.NoInputComplexOutputReturn = expected_result;
            serializer_mock.Setup(s => s.ToJson(expected_result, Moq.It.IsAny<SerializationOptions>())).Returns(json);
        };

        Because of = () => result = invoker.Invoke(base_url, service_instance, uri, parameters);

        It should_call_method_on_service_instance = () => service_instance.NoInputComplexOutputCalled.ShouldBeTrue();
        It should_return_serialized_result = () => result.ShouldEqual(json);
    }
}
