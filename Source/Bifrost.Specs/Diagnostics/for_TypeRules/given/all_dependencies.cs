using Bifrost.Diagnostics;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Diagnostics.for_TypeRules.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<IProblemsFactory> problems_factory_mock;
        protected static Mock<IProblemsReporter> problems_reporter_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
            problems_factory_mock = new Mock<IProblemsFactory>();
            problems_reporter_mock = new Mock<IProblemsReporter>();
        };
    }
}
