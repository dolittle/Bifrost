using Bifrost.Web.Services;
using Machine.Specifications;
using It = Machine.Specifications.It;
using System;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_without_parameter_and_target_method_expects_a_parameter : given.a_rest_service_method_invoker_and_a_service_call_for_complex_input_and_no_output
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_throw_parameter_count_mismatch_exception = () => exception.ShouldBeOfType<ParameterCountMismatchException>();
    }
}
