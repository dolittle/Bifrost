using Bifrost.Serialization;
using Bifrost.Services.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker.given
{
    public class a_rest_service_method_invoker
    {
        protected static Mock<ISerializer> serializer_mock;
        protected static RestServiceMethodInvoker invoker;

        Establish context = () =>
        {
            serializer_mock = new Mock<ISerializer>();
            invoker = new RestServiceMethodInvoker(serializer_mock.Object);
        };
    }
}
