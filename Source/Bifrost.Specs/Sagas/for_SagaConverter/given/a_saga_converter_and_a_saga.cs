using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
    public class a_saga_converter_and_a_saga
    {
        protected static SagaWithOneChapterProperty saga;
        protected const string expected_key = "A_Magical_Key";
        protected const string expected_partition = "A_Magical_Partition";
        protected static SagaConverter saga_converter;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ISerializer> serializer_mock;

        Establish context = () =>
                                {
                                    saga = new SagaWithOneChapterProperty {Key = expected_key, Partition = expected_partition};

                                    container_mock = new Mock<IContainer>();
                                    serializer_mock = new Mock<ISerializer>();
                                    saga_converter = new SagaConverter(container_mock.Object, serializer_mock.Object);
                                };
    }
}
