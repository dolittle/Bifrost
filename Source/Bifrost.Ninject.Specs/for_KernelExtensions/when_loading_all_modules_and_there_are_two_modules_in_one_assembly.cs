using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Ninject;
using Ninject.Modules;

namespace Bifrost.Ninject.Specs.for_KernelExtensions
{
    public class when_loading_all_modules_and_there_are_two_modules_in_one_assembly : given.a_kernel_and_a_type_discoverer
    {
        static IEnumerable<INinjectModule>    result;

        Establish context = () => 
        {
            type_discoverer_mock.Setup(t => t.FindMultiple<NinjectModule>()).Returns(new [] { typeof(FirstModule), typeof(SecondModule) });
            kernel_mock.Setup(k=>k.Load(Moq.It.IsAny<IEnumerable<INinjectModule>>())).Callback((IEnumerable<INinjectModule> a) => result = a);
        };

        Because of = () => kernel_mock.Object.LoadAllModules();

        It should_load_modules_from_one_assembly = () => result.Count().ShouldEqual(1);
    }
}
