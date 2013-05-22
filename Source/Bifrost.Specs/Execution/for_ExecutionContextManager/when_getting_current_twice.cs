using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager
{
    public class when_getting_current_twice : given.an_execution_context_manager
    {
        static  IExecutionContext   current_execution_context = null;

        static Mock<IExecutionContext>  execution_context;

        Establish context = () =>
        {
            call_context_mock.Setup(c => c.GetData<IExecutionContext>(ExecutionContextManager.ExecutionContextKey)).Returns(current_execution_context);
            call_context_mock.Setup(c => c.HasData(ExecutionContextManager.ExecutionContextKey)).Returns(()=>current_execution_context != null);
            call_context_mock.Setup(c => c.SetData(ExecutionContextManager.ExecutionContextKey, Moq.It.IsAny<object>())).Callback((string k, object o) => current_execution_context = (IExecutionContext)o);

            execution_context = new Mock<IExecutionContext>();
            execution_context_factory_mock.Setup(e => e.Create()).Returns(execution_context.Object);
        };

        Because of = () =>
        {
            var current = manager.Current;
            current = manager.Current;
        };

        It should_ask_the_factory_create_only_once = () => execution_context_factory_mock.Verify(e => e.Create(), Moq.Times.Once());
    }
}
