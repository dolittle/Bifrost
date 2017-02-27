using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_TypeImporter.given
{
    public class a_type_importer
    {
        protected static TypeImporter type_importer;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    type_importer = new TypeImporter(container_mock.Object,type_discoverer_mock.Object);
                                };
    }
}
