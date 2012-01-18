using System;
using Machine.Specifications;
using System.Collections.Specialized;
using Bifrost.Services.Execution;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class when_invoking_without_a_method : given.a_rest_service_method_invoker
    {
        const string base_url = "ServiceWithoutMethod";
        static Exception exception;
        static ServiceWithoutMethods service_instance;
        static Uri uri;
        static NameValueCollection parameters;

        Establish context = () =>
        {
            service_instance = new ServiceWithoutMethods();
            uri = new Uri(string.Format("http://localhost/{0}", base_url));
            parameters = new NameValueCollection();
        };

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_throw_method_not_specified_exception = () => exception.ShouldBeOfType<MethodNotSpecifiedException>();
    }
}
