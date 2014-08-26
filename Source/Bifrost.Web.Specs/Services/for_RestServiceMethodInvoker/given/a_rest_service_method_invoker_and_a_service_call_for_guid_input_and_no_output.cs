using System;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker.given
{
    public class a_rest_service_method_invoker_and_a_service_call_for_guid_input_and_no_output : a_rest_service_method_invoker_and_a_service_call
    {
        Establish context = () => uri = new Uri(string.Format("http://localhost/{0}/{1}", base_url, ServiceWithMethods.GuidInputNoOutputMethod));
    }
}
