using System;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker
{
    public class when_invoking_with_a_method_with_no_in_or_output : given.a_rest_service_method_invoker_and_a_service_call
    {
        Establish context = () => uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.NoInputOrOutputMethod));

        Because of = () => invoker.Invoke(base_url, service_instance, uri, parameters);

        It should_invoke_the_method_on_the_service_instance = () => service_instance.NoInputOrOutputCalled.ShouldBeTrue();
    }
}
