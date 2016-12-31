using System.Reflection;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Ninject.Modules;

namespace Bifrost.Ninject.Specs.for_KernelExtensions
{
    public class when_loading_all_modules_and_there_is_no_assemblies : given.a_kernel_and_a_type_discoverer
    {
        Because of = () => kernel_mock.Object.LoadAllModules();

        It should_not_result_in_loading_any_modules = () => kernel_mock.Verify(k => k.Load(Moq.It.IsAny<IEnumerable<INinjectModule>>()), Times.Never());
    }
}
