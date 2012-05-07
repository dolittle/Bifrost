using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
	[Subject(Subjects.resolving)]
	public class when_asking_if_it_can_resolve_a_resource_implementation : given.a_resource_convention
	{
		static bool can_resolve;

		Because of = () => can_resolve = convention.CanResolve(container_mock.Object, typeof(MyResources));

		It should_be_true = () => can_resolve.ShouldBeTrue();
	}
}