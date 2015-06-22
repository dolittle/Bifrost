using System.Dynamic;
using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ExecutionContextDetailsPopulator
{
    public class when_populating_and_two_populators_exist : dependency_injection
    {
        static ExecutionContextDetailsPopulator populator;
        static ExpandoObject details;

        static Mock<ICanPopulateExecutionContextDetails>    first_populator;
        static Mock<ICanPopulateExecutionContextDetails>    second_populator;

        Establish context = () =>
        {
            details = new ExpandoObject();
            GetMock<ITypeDiscoverer>().Setup(t => t.FindMultiple<ICanPopulateExecutionContextDetails>()).Returns(new[] { typeof(string), typeof(object) });

            first_populator = new Mock<ICanPopulateExecutionContextDetails>();
            second_populator = new Mock<ICanPopulateExecutionContextDetails>();

            GetMock<IContainer>().Setup(c => c.Get(typeof(string))).Returns(first_populator.Object);
            GetMock<IContainer>().Setup(c => c.Get(typeof(object))).Returns(second_populator.Object);

            populator = Get<ExecutionContextDetailsPopulator>();
        };

        Because of = () => populator.Populate(Get<IExecutionContext>(), details);

        It should_call_populate_on_first_populator = () => first_populator.Verify(p => p.Populate(Get<IExecutionContext>(), details));
        It should_call_populate_on_second_populator = () => second_populator.Verify(p => p.Populate(Get<IExecutionContext>(), details));
    }
}