using Bifrost.Serialization;
using Bifrost.Web.Services;
using Machine.Specifications;
using Moq;

namespace Bifrost.Web.Specs.Services.for_RestServiceMethodInvoker.given
{
    public class a_rest_service_method_invoker
    {
        protected static Mock<ISerializer> serializer_mock;
        protected static Mock<IJsonInterceptor> json_interceptor_mock;

        protected static RestServiceMethodInvoker invoker;

        Establish context = () =>
        {
            serializer_mock = new Mock<ISerializer>(); 
            json_interceptor_mock = new Mock<IJsonInterceptor>();
            json_interceptor_mock.Setup(m => m.Intercept(Moq.It.IsAny<string>())).Returns<string>(json => json);
            json_interceptor_mock.Setup(m => m.Intercept(Moq.It.IsAny<string>())).Returns<string>(json => json);
            invoker = new RestServiceMethodInvoker(serializer_mock.Object, json_interceptor_mock.Object);

        };
    }
}
