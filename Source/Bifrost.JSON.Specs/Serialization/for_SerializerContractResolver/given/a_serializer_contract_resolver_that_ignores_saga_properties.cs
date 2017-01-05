using Bifrost.Execution;
using Bifrost.JSON.Serialization;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.JSON.Specs.Serialization.for_SerializerContractResolver.given
{
    public class a_serializer_contract_resolver_that_ignores_saga_properties
    {
        protected static SerializerContractResolver contract_resolver;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    contract_resolver = new SerializerContractResolver(container_mock.Object, new SagaSerializationOptions());
                                };
    }
}
