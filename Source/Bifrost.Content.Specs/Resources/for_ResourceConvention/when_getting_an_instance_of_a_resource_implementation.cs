using System;
using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
    [Subject(Subjects.getting_instance)]
    public class when_getting_an_instance_of_a_resource_implementation : given.a_resource_convention
    {
        static MyResources resources_instance;

        Establish context = () => container_mock.Setup(c => c.Bind(typeof(MyResources), Moq.It.IsAny<object>())).Callback((Type serviceType, object instance) => resources_instance = (MyResources)instance);

        Because of = () => convention.Resolve(container_mock.Object, typeof(MyResources));

        It should_be_inheriting_my_strings = () => resources_instance.GetType().BaseType.ShouldEqual(typeof (MyResources));
        It should_be_a_proxy_type = () => resources_instance.GetType().ShouldNotEqual(typeof (MyResources));
    }
}