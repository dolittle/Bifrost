using Bifrost.Commands;
using Bifrost.Events;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Commands.for_CommandContextFactory.given
{
    public class a_command_context_factory
    {
        protected static CommandContextFactory factory;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator;
        protected static Mock<IProcessMethodInvoker> process_method_invoker_mock;
        protected static Mock<ISagaLibrarian> saga_librarian_mock;
        protected static Mock<IExecutionContextManager> execution_context_manager_mock;
        protected static Mock<IEventStore> event_store_mock;

        Establish context = () =>
                                {
                                    uncommitted_event_stream_coordinator = new Mock<IUncommittedEventStreamCoordinator>();
                                    event_store_mock = new Mock<IEventStore>();
                                    process_method_invoker_mock = new Mock<IProcessMethodInvoker>();
                                    saga_librarian_mock = new Mock<ISagaLibrarian>();
                                    execution_context_manager_mock = new Mock<IExecutionContextManager>();
                                           
                                    factory = new CommandContextFactory(
                                        uncommitted_event_stream_coordinator.Object, 
                                        saga_librarian_mock.Object,
                                        process_method_invoker_mock.Object,
                                        execution_context_manager_mock.Object,
                                        event_store_mock.Object);
                                };
    }
}
