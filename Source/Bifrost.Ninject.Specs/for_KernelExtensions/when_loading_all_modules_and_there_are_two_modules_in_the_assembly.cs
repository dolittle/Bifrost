using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Ninject.Modules;

namespace Bifrost.Ninject.Specs.for_KernelExtensions
{
    public class when_loading_all_modules_and_there_are_two_modules_in_the_assembly : given.a_kernel_and_a_type_importer
    {
        static IEnumerable<INinjectModule>    result;

        Establish context = () => 
        {
            type_importer_mock.Setup(t => t.ImportMany<NinjectModule>()).Returns(new NinjectModule[] { new FirstModule(), new SecondModule() });
            kernel_mock.Setup(k=>k.Load(Moq.It.IsAny<IEnumerable<INinjectModule>>())).Callback((IEnumerable<INinjectModule> a) => result = a);
        };

        Because of = () => kernel_mock.Object.LoadAllModules();

        It should_load_all_modules = () => result.Count().ShouldEqual(2);
    }
}
