using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager
{
    public class when_getting_current_twice : given.an_execution_context_manager
    {
        static IExecutionContext current_execution_context;

        Establish context = () =>
        {
            GetMock<ICallContext>().Setup(c => c.GetData<IExecutionContext>(ExecutionContextManager.ExecutionContextKey)).Returns(current_execution_context);
            GetMock<ICallContext>().Setup(c => c.HasData(ExecutionContextManager.ExecutionContextKey)).Returns(() => current_execution_context != null);
            GetMock<ICallContext>().Setup(c => c.SetData(ExecutionContextManager.ExecutionContextKey, Moq.It.IsAny<object>())).Callback((string k, object o) => current_execution_context = (IExecutionContext) o);

            GetMock<IExecutionContextFactory>().Setup(e => e.Create()).Returns(Get<IExecutionContext>);
        };

        Because of = () =>
        {
            var current = manager.Current;
            current = manager.Current;
        };

        It should_ask_the_factory_create_only_once = () => GetMock<IExecutionContextFactory>().Verify(e => e.Create(), Moq.Times.Once());
    }
}
