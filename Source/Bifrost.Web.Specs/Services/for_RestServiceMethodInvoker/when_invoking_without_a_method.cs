using System;
using Bifrost.Web.Services;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker
{
    public class when_invoking_without_a_method : given.a_rest_service_method_invoker_and_a_service_call
    {
        static Exception exception;

        Establish context = () => uri = new Uri(string.Format("http://localhost/{0}", base_url));

        Because of = () => exception = Catch.Exception(() => invoker.Invoke(base_url, service_instance, uri, parameters));

        It should_throw_method_not_specified_exception = () => exception.ShouldBeOfType<MethodNotSpecifiedException>();
    }
}
