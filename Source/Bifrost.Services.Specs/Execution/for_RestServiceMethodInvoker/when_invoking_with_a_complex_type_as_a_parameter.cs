using System;
using System.Collections.Specialized;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_with_a_complex_type_as_a_parameter : given.a_rest_service_method_invoker
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
