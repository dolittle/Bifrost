using Bifrost.Commands;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandHandlerManager.given
{
    public class a_command_handler_manager
    {
        protected static CommandHandlerManager manager;
        protected static Mock<ITypeImporter> type_importer_mock;

            Establish context = () =>
                                {
                                    type_importer_mock = new Mock<ITypeImporter>();
                                    manager = new CommandHandlerManager(type_importer_mock.Object);
                                };
    }
}
