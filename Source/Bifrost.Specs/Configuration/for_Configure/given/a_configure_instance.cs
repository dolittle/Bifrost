using Bifrost.Configuration;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using Bifrost.Fakes.Entities;

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
    	protected static Mock<ISagasConfiguration> sagas_configuration_mock;
		protected static Mock<ISerializationConfiguration> serialization_configuration_mock;
        protected static Mock<IApplicationManager> application_manager_mock;
        protected static Mock<IApplication> application_mock;
        protected static Mock<IViewsConfiguration> views_configuration_mock;
        protected static Mock<IDefaultStorageConfiguration> default_storage_configuration_mock;

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

                                    views_configuration_mock = new Mock<IViewsConfiguration>();
                                    container_mock.Setup(c => c.Get<IViewsConfiguration>()).Returns(views_configuration_mock.Object);

									sagas_configuration_mock = new Mock<ISagasConfiguration>();
                                	container_mock.Setup(c => c.Get<ISagasConfiguration>()).Returns(sagas_configuration_mock.Object);
			
									serialization_configuration_mock = new Mock<ISerializationConfiguration>();
									container_mock.Setup(c => c.Get<ISerializationConfiguration>()).Returns(serialization_configuration_mock.Object);

                                    application_mock = new Mock<IApplication>();
                                    application_manager_mock = new Mock<IApplicationManager>();
                                    application_manager_mock.Setup(a => a.Get()).Returns(application_mock.Object);
                                    container_mock.Setup(c => c.Get<IApplicationManager>()).Returns(application_manager_mock.Object);

                                    default_storage_configuration_mock = new Mock<IDefaultStorageConfiguration>();
                                    container_mock.Setup(c => c.Get<IDefaultStorageConfiguration>()).Returns(default_storage_configuration_mock.Object);

                                    configure_instance = Configure.With(container_mock.Object, default_conventions_mock.Object, default_bindings_mock.Object);
                                };
    }
}
