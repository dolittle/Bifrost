using System.Collections.Generic;
using Bifrost.Configuration;
using Bifrost.Configuration.Assemblies;
using Bifrost.Configuration.Defaults;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Tenancy;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_Configure.given
{
    public class a_configure_instance
    {
        protected static Configure configure_instance;
        protected static Mock<IContainer> container;
        protected static Mock<ICommandsConfiguration> commands_configuration;
        protected static Mock<IDefaultConventions> default_conventions;
        protected static Mock<IDefaultBindings> default_bindings;
        protected static Mock<IEventsConfiguration> events_configuration;
        protected static Mock<ITasksConfiguration> tasks_configuration;
        protected static Mock<ISerializationConfiguration> serialization_configuration;
        protected static Mock<IViewsConfiguration> views_configuration;
        protected static Mock<IDefaultStorageConfiguration> default_storage_configuration;
        protected static Mock<IFrontendConfiguration> frontend_configuration;
        protected static Mock<ICallContextConfiguration> call_context_configuration;
        protected static Mock<IExecutionContextConfiguration> execution_context_configuration;
        protected static Mock<ISecurityConfiguration> security_configuration;
        protected static Mock<ITenancyConfiguration> tenancy_configuration;
        protected static Mock<ITypeImporter> type_importer;
        protected static Mock<IInstancesOf<ICanConfigure>> configurators;
        protected static Mock<IInstancesOf<IWantToKnowWhenConfigurationIsDone>> after_configuration_callbacks;
        

        Establish context = () =>
                                {
                                    Configure.Reset();
                                    container = new Mock<IContainer>();
                                    default_conventions = new Mock<IDefaultConventions>();
                                    default_bindings = new Mock<IDefaultBindings>();

                                    commands_configuration = new Mock<ICommandsConfiguration>();
                                    container.Setup(c => c.Get<ICommandsConfiguration>()).Returns(commands_configuration.Object);

                                    events_configuration = new Mock<IEventsConfiguration>();
                                    container.Setup(c => c.Get<IEventsConfiguration>()).Returns(events_configuration.Object);

                                    tasks_configuration = new Mock<ITasksConfiguration>();
                                    container.Setup(c => c.Get<ITasksConfiguration>()).Returns(tasks_configuration.Object);

                                    views_configuration = new Mock<IViewsConfiguration>();
                                    container.Setup(c => c.Get<IViewsConfiguration>()).Returns(views_configuration.Object);
           
                                    serialization_configuration = new Mock<ISerializationConfiguration>();
                                    container.Setup(c => c.Get<ISerializationConfiguration>()).Returns(serialization_configuration.Object);

                                    default_storage_configuration = new Mock<IDefaultStorageConfiguration>();
                                    container.Setup(c => c.Get<IDefaultStorageConfiguration>()).Returns(default_storage_configuration.Object);

                                    frontend_configuration = new Mock<IFrontendConfiguration>();
                                    container.Setup(c => c.Get<IFrontendConfiguration>()).Returns(frontend_configuration.Object);

                                    call_context_configuration = new Mock<ICallContextConfiguration>();
                                    container.Setup(c => c.Get<ICallContextConfiguration>()).Returns(call_context_configuration.Object);

                                    execution_context_configuration = new Mock<IExecutionContextConfiguration>();
                                    container.Setup(c => c.Get<IExecutionContextConfiguration>()).Returns(execution_context_configuration.Object);

                                    security_configuration = new Mock<ISecurityConfiguration>();
                                    container.Setup(c => c.Get<ISecurityConfiguration>()).Returns(security_configuration.Object);

                                    tenancy_configuration = new Mock<ITenancyConfiguration>();
                                    container.Setup(c => c.Get<ITenancyConfiguration>()).Returns(tenancy_configuration.Object);

                                    type_importer = new Mock<ITypeImporter>();
                                    container.Setup(c => c.Get<ITypeImporter>()).Returns(type_importer.Object);

                                    configure_instance = Configure.With(container.Object, default_conventions.Object, default_bindings.Object, new AssembliesConfiguration(null));
                                    
                                    configurators = new Mock<IInstancesOf<ICanConfigure>>();
                                    configurators.Setup(c => c.GetEnumerator()).Returns(new List<ICanConfigure>().GetEnumerator());
                                    container.Setup(c => c.Get<IInstancesOf<ICanConfigure>>()).Returns(configurators.Object);

                                    after_configuration_callbacks = new Mock<IInstancesOf<IWantToKnowWhenConfigurationIsDone>>();
                                    after_configuration_callbacks.Setup(a => a.GetEnumerator()).Returns(new List<IWantToKnowWhenConfigurationIsDone>().GetEnumerator());
                                    container.Setup(c => c.Get<IInstancesOf<IWantToKnowWhenConfigurationIsDone>>()).Returns(after_configuration_callbacks.Object);
                                };
    }
}
