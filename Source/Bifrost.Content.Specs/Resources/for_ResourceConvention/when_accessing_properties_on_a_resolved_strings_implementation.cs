using System;
using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
	[Subject(Subjects.accessing_properties)]
	public class when_accessing_virtual_properties_on_a_resolved_resource_implementation : given.a_resource_convention
	{
		static MyResources resources_instance;

        Establish context = () => container_mock.Setup(c => c.Bind(typeof(MyResources), Moq.It.IsAny<object>())).Callback((Type serviceType, object instance) => resources_instance = (MyResources)instance);

	    Because of = () =>
	                     {
	                         convention.Resolve(container_mock.Object, typeof (MyResources));
	                         var result = resources_instance.Something;
	                     };

		It should_call_the_interceptor = () => interceptor.InterceptCalled.ShouldBeTrue();
	}
}