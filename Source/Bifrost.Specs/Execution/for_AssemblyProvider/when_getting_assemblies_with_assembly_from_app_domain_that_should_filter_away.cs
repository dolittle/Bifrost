using System.Runtime.InteropServices;
using Bifrost.Collections;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_AssemblyProvider
{
    public class when_getting_assemblies_with_assembly_from_app_domain_that_should_filter_away : given.an_unitialized_assembly_provider
    {
        const string assembly_name = "MyAssembly";
        static FakeAssembly assembly;

        static IObservableCollection<_Assembly>  result;

        Establish context = () =>
        {
            assembly = new FakeAssembly();
            assembly.AssemblyNameToReturn = assembly_name;
            
            app_domain_mock.Setup(a => a.GetAssemblies()).Returns(new[] { assembly });
            assembly_filters_mock.Setup(a => a.ShouldInclude(assembly_name)).Returns(false);

            initialize();
        };

        Because of = () => result = provider.GetAll();

        It should_not_contain_the_assembly_from_the_app_domain = () => result.ShouldNotContain(assembly);
    }
}