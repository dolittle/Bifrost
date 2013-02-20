using Bifrost.Web.Services;
using Machine.Specifications;
using It = Machine.Specifications.It;
using System;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_with_underscore_parameter_and_target_method_has_no_parameter : given.a_rest_service_method_invoker_and_a_service_call
    {
        static Exception exception;

        Establish context = () =>
        {
            uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.NoInputOrOutputMethod));
            parameters.Add("_", "Something");
        };

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_not_throw_parameter_count_mismatch_exception = () => exception.ShouldBeNull();
    }
}
