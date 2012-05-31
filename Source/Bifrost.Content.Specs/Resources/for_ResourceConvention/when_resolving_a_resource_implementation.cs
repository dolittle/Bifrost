using System;
using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
	[Subject(Subjects.resolving)]
	public class when_resolving_a_resource_implementation : given.a_resource_convention
	{
	    static MyResources resources_instance;

        Establish context = () => container_mock.Setup(c => c.Bind(typeof (MyResources), Moq.It.IsAny<object>())).Callback((Type type, object instance)=>resources_instance = instance as MyResources);

		Because of = () => convention.Resolve(container_mock.Object, typeof(MyResources));

	    It should_bind_to_the_container = () => resources_instance.ShouldNotBeNull();
	}
}
