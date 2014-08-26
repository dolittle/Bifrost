using Machine.Specifications;
using System;
using System.Collections.Specialized;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker
{
    public class when_invoking_and_method_does_not_exist : given.a_rest_service_method_invoker
    {
        const string base_url = "ServiceWithoutMethod";
        const string method_name = "SomeMethod";
        static ServiceWithoutMethods service_instance;
        static Uri uri;
        static NameValueCollection  parameters;

        static Exception exception;

        Establish context = () => {
            service_instance = new ServiceWithoutMethods();
            uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, method_name));
            parameters = new NameValueCollection();
        };

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_throw_missing_method_exception = () => exception.ShouldBeOfType<MissingMethodException>();
    }
}
