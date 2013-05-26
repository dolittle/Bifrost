using System.Dynamic;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ExecutionContextDetailsPopulator
{
    public class when_populating_and_two_populators_exist
    {
        static Mock<ITypeDiscoverer> type_discoverer_mock;
        static ExecutionContextDetailsPopulator populator;
        static Mock<IExecutionContext> execution_context_mock;
        static Mock<IContainer> container_mock;
        static ExpandoObject details;

        static Mock<ICanPopulateExecutionContextDetails>    first_populator;
        static Mock<ICanPopulateExecutionContextDetails>    second_populator;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            execution_context_mock = new Mock<IExecutionContext>();
            details = new ExpandoObject();
            container_mock = new Mock<IContainer>();

            type_discoverer_mock.Setup(t => t.FindMultiple<ICanPopulateExecutionContextDetails>()).Returns(new[] { typeof(string), typeof(object) });

            first_populator = new Mock<ICanPopulateExecutionContextDetails>();
            second_populator = new Mock<ICanPopulateExecutionContextDetails>();

            container_mock.Setup(c => c.Get(typeof(string))).Returns(first_populator.Object);
            container_mock.Setup(c => c.Get(typeof(object))).Returns(second_populator.Object);

            populator = new ExecutionContextDetailsPopulator(type_discoverer_mock.Object, container_mock.Object);
        };

        Because of = () => populator.Populate(execution_context_mock.Object, details);

        It should_call_populate_on_first_populator = () => first_populator.Verify(p => p.Populate(execution_context_mock.Object, details));
        It should_call_populate_on_second_populator = () => second_populator.Verify(p => p.Populate(execution_context_mock.Object, details));
    }
}