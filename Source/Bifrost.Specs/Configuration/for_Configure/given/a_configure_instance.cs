using Bifrost.Configuration;
using Bifrost.Configuration.Assemblies;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_Configure.given
{
    public class a_configure_instance
    {
        protected static Configure configure_instance;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ICommandsConfiguration> commands_configuration_mock;
        protected static Mock<IDefaultConventions> default_conventions_mock;
        protected static Mock<IDefaultBindings> default_bindings_mock;
        protected static Mock<IEventsConfiguration> events_configuration_mock;
        protected static Mock<ITasksConfiguration> tasks_configuration_mock;
    	protected static Mock<ISagasConfiguration> sagas_configuration_mock;
		protected static Mock<ISerializationConfiguration> serialization_configuration_mock;
        protected static Mock<IViewsConfiguration> views_configuration_mock;
        protected static Mock<IDefaultStorageConfiguration> default_storage_configuration_mock;
        protected static Mock<IFrontendConfiguration> frontend_configuration_mock;
        protected static Mock<ICallContextConfiguration> call_context_configuration_mock;
        protected static Mock<IExecutionContextConfiguration> execution_context_configuration_mock;
        protected static Mock<ISecurityConfiguration> security_configuration_mock;
        protected static Mock<ITypeImporter> type_importer_mock;

        Establish context = () =>
                                {
                                    Configure.Reset();
                                    container_mock = new Mock<IContainer>();
                                    default_conventions_mock = new Mock<IDefaultConventions>();
                                    default_bindings_mock = new Mock<IDefaultBindings>();

                                    commands_configuration_mock = new Mock<ICommandsConfiguration>();
                                    container_mock.Setup(c => c.Get<ICommandsConfiguration>()).Returns(commands_configuration_mock.Object);

                                    events_configuration_mock = new Mock<IEventsConfiguration>();
                                    container_mock.Setup(c => c.Get<IEventsConfiguration>()).Returns(events_configuration_mock.Object);

                                    tasks_configuration_mock = new Mock<ITasksConfiguration>();
                                    container_mock.Setup(c => c.Get<ITasksConfiguration>()).Returns(tasks_configuration_mock.Object);

                                    views_configuration_mock = new Mock<IViewsConfiguration>();
                                    container_mock.Setup(c => c.Get<IViewsConfiguration>()).Returns(views_configuration_mock.Object);

									sagas_configuration_mock = new Mock<ISagasConfiguration>();
                                	container_mock.Setup(c => c.Get<ISagasConfiguration>()).Returns(sagas_configuration_mock.Object);
			
									serialization_configuration_mock = new Mock<ISerializationConfiguration>();
									container_mock.Setup(c => c.Get<ISerializationConfiguration>()).Returns(serialization_configuration_mock.Object);

                                    default_storage_configuration_mock = new Mock<IDefaultStorageConfiguration>();
                                    container_mock.Setup(c => c.Get<IDefaultStorageConfiguration>()).Returns(default_storage_configuration_mock.Object);

                                    frontend_configuration_mock = new Mock<IFrontendConfiguration>();
                                    container_mock.Setup(c => c.Get<IFrontendConfiguration>()).Returns(frontend_configuration_mock.Object);

                                    call_context_configuration_mock = new Mock<ICallContextConfiguration>();
                                    container_mock.Setup(c => c.Get<ICallContextConfiguration>()).Returns(call_context_configuration_mock.Object);

                                    execution_context_configuration_mock = new Mock<IExecutionContextConfiguration>();
                                    container_mock.Setup(c => c.Get<IExecutionContextConfiguration>()).Returns(execution_context_configuration_mock.Object);

                                    security_configuration_mock = new Mock<ISecurityConfiguration>();
                                    container_mock.Setup(c => c.Get<ISecurityConfiguration>()).Returns(security_configuration_mock.Object);

                                    type_importer_mock = new Mock<ITypeImporter>();
                                    container_mock.Setup(c => c.Get<ITypeImporter>()).Returns(type_importer_mock.Object);

                                    configure_instance = Configure.With(container_mock.Object, default_conventions_mock.Object, default_bindings_mock.Object, new AssembliesConfiguration(null));
                                };
    }
}
