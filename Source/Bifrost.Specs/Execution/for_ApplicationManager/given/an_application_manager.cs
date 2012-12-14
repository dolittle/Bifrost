using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_ApplicationManager.given
{
    public class an_application_manager
    {
        protected static ApplicationManager application_manager;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                            {
                                type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                container_mock = new Mock<IContainer>();
                                application_manager = new ApplicationManager(type_discoverer_mock.Object, container_mock.Object);
                            };
    }
}
