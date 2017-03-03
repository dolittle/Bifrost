using Bifrost.Configuration;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_SagasConfiguration.given
{
    public class a_sagas_configuration_and_container_object : a_sagas_configuration
    {
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                };
    }
}