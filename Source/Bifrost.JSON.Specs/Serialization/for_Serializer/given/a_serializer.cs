using Bifrost.Execution;
using Bifrost.JSON.Serialization;
using Machine.Specifications;
using Moq;

namespace Bifrost.JSON.Specs.Serialization.for_Serializer.given
{
    public class a_serializer
    {
        protected static Serializer serializer;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    serializer = new Serializer(container_mock.Object);
                                };
    }
}
