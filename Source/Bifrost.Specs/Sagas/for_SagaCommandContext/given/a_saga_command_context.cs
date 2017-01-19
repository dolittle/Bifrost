using System.Security.Principal;
using Bifrost.Commands;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext.given
{
    public class a_saga_command_context
    {
        protected static SagaCommandContext command_context;
        protected static Mock<ISaga> saga_mock;
        protected static Mock<ICommand> command_mock;
        protected static Mock<IEventStore> event_store_mock;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator_mock;
        protected static Mock<IProcessMethodInvoker> process_method_invoker_mock;
        protected static Mock<ISagaLibrarian> saga_librarian_mock;
        protected static Mock<IExecutionContextManager> execution_context_manager_mock;
        protected static Mock<IExecutionContext> execution_context_mock;
        protected static Mock<IPrincipal> principal_mock;
        protected static Mock<IIdentity> identity_mock;

        Establish context = () =>
                                {
                                    saga_mock = new Mock<ISaga>();
                                    command_mock = new Mock<ICommand>();
                                    event_store_mock = new Mock<IEventStore>();
                                    uncommitted_event_stream_coordinator_mock = new Mock<IUncommittedEventStreamCoordinator>();
                                    process_method_invoker_mock = new Mock<IProcessMethodInvoker>();
                                    saga_librarian_mock = new Mock<ISagaLibrarian>();
                                    execution_context_manager_mock = new Mock<IExecutionContextManager>();
                                    execution_context_mock = new Mock<IExecutionContext>();
                                    principal_mock = new Mock<IPrincipal>();
                                    identity_mock = new Mock<IIdentity>();
                                    identity_mock.SetupGet(i => i.Name).Returns("Bifrost");
                                    principal_mock.SetupGet(p => p.Identity).Returns(identity_mock.Object);
                                    execution_context_mock.Setup(e => e.Principal).Returns(principal_mock.Object);
                                    execution_context_mock.Setup(e => e.System).Returns("[Specs]");
                                    command_context = new SagaCommandContext(
                                            saga_mock.Object,
                                            command_mock.Object,
                                            execution_context_mock.Object,
                                            event_store_mock.Object,
                                            uncommitted_event_stream_coordinator_mock.Object,
                                            process_method_invoker_mock.Object,
                                            saga_librarian_mock.Object
                                        );
                                };
    }
}
