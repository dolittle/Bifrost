using System;
using System.Collections.Specialized;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Bifrost.Web.Services;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_with_wrong_parameter_name : given.a_rest_service_method_invoker_and_a_service_call_for_complex_input_and_no_output
    {
        static Exception exception;

        Establish context = () => parameters.Add("something", "{}");

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_throw_missing_parameter_exception = () => exception.ShouldBeOfType<MissingParameterException>();
    }
}
