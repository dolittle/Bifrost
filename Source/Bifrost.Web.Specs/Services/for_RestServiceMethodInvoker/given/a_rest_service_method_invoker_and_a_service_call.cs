using System;
using System.Collections.Specialized;
using Machine.Specifications;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker.given
{
    public class a_rest_service_method_invoker_and_a_service_call : a_rest_service_method_invoker
    {
        protected const string base_url = "ServiceWithMethods";
        protected static ServiceWithMethods service_instance;
        protected static Uri uri;
        protected static NameValueCollection parameters;

        Establish context = () =>
        {
            service_instance = new ServiceWithMethods();
            uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.ComplexInputNoOutputMethod));
            parameters = new NameValueCollection();
        };
    }
}
