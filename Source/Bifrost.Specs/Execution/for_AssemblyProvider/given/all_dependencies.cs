using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_AssemblyProvider.given
{
    public class all_dependencies
    {
        protected static Mock<_AppDomain> app_domain_mock;
        protected static Mock<IAssemblyFilters> assembly_filters_mock;
        protected static Mock<IFileSystem> file_system_mock;
        protected static Mock<IExecutionEnvironment> execution_environment_mock;
        protected static Mock<IAssemblyUtility> assembly_utility_mock;

        Establish context = () =>
        {
            app_domain_mock = new Mock<_AppDomain>();
            assembly_filters_mock = new Mock<IAssemblyFilters>();
            file_system_mock = new Mock<IFileSystem>();
            execution_environment_mock = new Mock<IExecutionEnvironment>();
            assembly_utility_mock = new Mock<IAssemblyUtility>();
        };
    }
}
