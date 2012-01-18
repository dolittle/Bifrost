using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_BindingConventionManager.given
{

    public class a_binding_convention_manager
    {
        protected static BindingConventionManager manager;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    manager = new BindingConventionManager(container_mock.Object, type_discoverer_mock.Object);
                                };
    }
}
