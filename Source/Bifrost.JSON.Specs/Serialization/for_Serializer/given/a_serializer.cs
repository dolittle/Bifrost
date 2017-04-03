using Bifrost.Applications;
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
        protected static Mock<IApplicationResourceIdentifierConverter> application_resource_identifier_converter;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    application_resource_identifier_converter = new Mock<IApplicationResourceIdentifierConverter>();
                                    serializer = new Serializer(container_mock.Object, application_resource_identifier_converter.Object);
                                };
    }
}
