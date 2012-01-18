using Bifrost.Commands;
using Bifrost.Events;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Commands.for_CommandContextManager.given
{
	public class a_command_context_manager
	{
		protected static CommandContextManager Manager;
        protected static Mock<IEventStore> event_store_mock;
        protected static Mock<IProcessMethodInvoker> process_method_invoker_mock;
        protected static Mock<ISagaLibrarian> saga_librarian_mock;
        protected static Mock<IExecutionContextManager> execution_context_manager_mock;

		Establish context = () =>
		                            	{
											CommandContextManager.ResetContext();
		                            		event_store_mock = new Mock<IEventStore>();
		                            		process_method_invoker_mock = new Mock<IProcessMethodInvoker>();
		                            		saga_librarian_mock = new Mock<ISagaLibrarian>();
                                            execution_context_manager_mock = new Mock<IExecutionContextManager>();
                                           
		                            		Manager = new CommandContextManager(
												event_store_mock.Object, 
												saga_librarian_mock.Object,
												process_method_invoker_mock.Object,
                                                execution_context_manager_mock.Object);
		                            	};
	}
}
