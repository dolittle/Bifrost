using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationConfigurationBuilder
{
    public class when_building
    {
        const string application_name = "Some Application";

        static ApplicationConfigurationBuilder builder;
        static IApplication application;
        static Mock<IApplicationStructureConfigurationBuilder> application_structure_configuration_builder;
        static Mock<IApplicationStructure> application_structure;

        Establish context = () =>
        {
            application_structure = new Mock<IApplicationStructure>();
            application_structure_configuration_builder = new Mock<IApplicationStructureConfigurationBuilder>();
            application_structure_configuration_builder.Setup(a => a.Build()).Returns(application_structure.Object);
            builder = new ApplicationConfigurationBuilder(application_name, application_structure_configuration_builder.Object);
        };

        Because of = () => application = builder.Build();

        It should_return_an_application = () => application.ShouldNotBeNull();
        It should_hold_the_name_of_the_application = () => ((string) application.Name).ShouldEqual(application_name);
        It should_build_application_structure = () => application_structure_configuration_builder.Verify(a => a.Build(), Times.Once());
        It should_hold_the_built_application_structure = () => application.Structure.ShouldEqual(application_structure.Object);
    }
}
