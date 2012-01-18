using Bifrost.Execution;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Execution.for_ApplicationManager.given
{
    public class an_application_manager
    {
        protected static ApplicationManager application_manager;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IServiceLocator> service_locator_mock;

        Establish context = () =>
                            {
                                type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                service_locator_mock = new Mock<IServiceLocator>();
                                application_manager = new ApplicationManager(type_discoverer_mock.Object, service_locator_mock.Object);
                            };
    }
}
