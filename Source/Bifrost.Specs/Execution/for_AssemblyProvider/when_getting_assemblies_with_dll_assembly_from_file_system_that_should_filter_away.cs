using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Bifrost.Collections;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_AssemblyProvider
{
    public class when_getting_assemblies_with_dll_assembly_from_file_system_that_should_filter_away : given.an_unitialized_assembly_provider
    {
        const string assembly_file_name = "MyAssembly.dll";

        static AssemblyName assembly_name;
        static FileInfo file_info;
        static FakeAssembly assembly;

        static IObservableCollection<_Assembly>  result;

        Establish context = () =>
        {
            assembly = new FakeAssembly();
            assembly.AssemblyNameToReturn = assembly_file_name;

            assembly_name = new AssemblyName(assembly_file_name);

            file_info = new FileInfo(assembly_file_name);
            execution_environment_mock.Setup(e => e.GetReferencedAssembliesFileInfo()).Returns(new[] { file_info });
            assembly_utility_mock.Setup(a => a.GetAssemblyNameForFile(file_info)).Returns(assembly_name);
            assembly_utility_mock.Setup(a => a.Load(assembly_name)).Returns(assembly);
            assembly_utility_mock.Setup(a => a.IsAssembly(file_info)).Returns(true);

            assembly_filters_mock.Setup(a => a.ShouldInclude(assembly_file_name)).Returns(false);

            initialize();
        };

        Because of = () => result = provider.GetAll();

        It should_not_return_the_assembly_from_the_filesystem = () => result.ShouldNotContain(assembly);
    }
}