using System;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_InstancesOf
{
    public class when_having_multiple_implementations : dependency_injection
    {
        static IAmAnInterface[] instances;

        static OneImplementation one_implementation_instance;
        static SecondImplementation second_implemenation_instance;

        Establish context = () => 
        {
            GetMock<ITypeDiscoverer>().Setup(t => t.FindMultiple<IAmAnInterface>()).Returns(new Type[] {
                typeof(OneImplementation),
                typeof(SecondImplementation)
            });
            one_implementation_instance = new OneImplementation();
            GetMock<IContainer>().Setup(c => c.Get(typeof(OneImplementation))).Returns(one_implementation_instance);
            second_implemenation_instance = new SecondImplementation();
            GetMock<IContainer>().Setup(c => c.Get(typeof(SecondImplementation))).Returns(second_implemenation_instance);
        };

        Because of = () => instances = Get<InstancesOf<IAmAnInterface>>().ToArray();

        It should_get_the_implementations = () => instances.ShouldContainOnly(one_implementation_instance, second_implemenation_instance);
             
    }
}
