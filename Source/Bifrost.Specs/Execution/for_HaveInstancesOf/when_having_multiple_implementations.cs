using System;
using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_HaveInstancesOf
{
    public class when_having_multiple_implementations
    {
        static Mock<ITypeDiscoverer>   type_discoverer_mock;
        static Mock<IContainer> container_mock;
        static IAmAnInterface[] instances;

        static OneImplementation one_implementation_instance;
        static SecondImplementation second_implemenation_instance;

        Establish context = () => 
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            type_discoverer_mock.Setup(t => t.FindMultiple<IAmAnInterface>()).Returns(new Type[] {
                typeof(OneImplementation),
                typeof(SecondImplementation)
            });
            container_mock = new Mock<IContainer>();
            one_implementation_instance = new OneImplementation();
            container_mock.Setup(c => c.Get(typeof(OneImplementation))).Returns(one_implementation_instance);
            second_implemenation_instance = new SecondImplementation();
            container_mock.Setup(c => c.Get(typeof(SecondImplementation))).Returns(second_implemenation_instance);
        };

        Because of = () => instances = new HaveInstancesOf<IAmAnInterface>(type_discoverer_mock.Object, container_mock.Object).ToArray();

        It should_get_the_implementations = () => instances.ShouldContainOnly(one_implementation_instance, second_implemenation_instance);
             
    }
}
