using System;
using Machine.Specifications;
using System.Collections.Specialized;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_with_a_method_with_no_in_or_output : given.a_rest_service_method_invoker
    {
        const string base_url = "ServiceWithMethods";
        static ServiceWithMethods service_instance;
        static Uri uri;
        static NameValueCollection parameters;

        Establish context = () =>
        {
            service_instance = new ServiceWithMethods();
            uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.NoInputOrOutputMethod));
            parameters = new NameValueCollection();
        };

        Because of = () => invoker.Invoke(base_url, service_instance, uri, parameters);

        It should_invoke_the_method_on_the_service_instance = () => service_instance.NoInputOrOutputCalled.ShouldBeTrue();
    }
}
