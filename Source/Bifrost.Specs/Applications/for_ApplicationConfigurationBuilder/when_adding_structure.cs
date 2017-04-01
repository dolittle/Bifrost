using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationConfigurationBuilder
{
    public class when_adding_structure
    {
        const string application_name = "Some Application";
        static ApplicationConfigurationBuilder builder;
        static IApplicationConfigurationBuilder new_builder;
        static IApplicationStructureConfigurationBuilder structure_builder;

        Establish context = () => builder = new ApplicationConfigurationBuilder(application_name);

        Because of = () => new_builder = builder.Structure(s => structure_builder = s);

        It should_pass_application_structure_configuration_builder_to_callback = () => structure_builder.ShouldNotBeNull();
        It should_return_a_builder_with_the_same_name = () => ((string) new_builder.Name).ShouldEqual(application_name);
    }
}
