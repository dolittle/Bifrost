using Bifrost.Serialization;
using Bifrost.Web.Services;
using Machine.Specifications;
using Moq;

namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker.given
{
    public class a_rest_service_method_invoker
    {
        protected static Mock<ISerializer> serializer_mock;
        protected static Mock<IValueFilterInvoker> value_filter_invoker_mock;

        protected static RestServiceMethodInvoker invoker;

        Establish context = () =>
        {
            serializer_mock = new Mock<ISerializer>(); 
            value_filter_invoker_mock = new Mock<IValueFilterInvoker>();
            value_filter_invoker_mock.Setup(m => m.FilterInputValue(Moq.It.IsAny<string>())).Returns<string>(v => v);
            value_filter_invoker_mock.Setup(m => m.FilterOutputValue(Moq.It.IsAny<string>())).Returns<string>(v => v);
            invoker = new RestServiceMethodInvoker(serializer_mock.Object, value_filter_invoker_mock.Object);

        };
    }
}
