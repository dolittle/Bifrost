using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
	[Subject(Subjects.resolving)]
	public class when_asking_if_it_can_resolve_a_type_not_implementing_resource : given.a_resource_convention
	{
		static bool can_resolve;

		Because of = () =>
		             	{
		             		var service = typeof(MyStringsNotImplementingStrings);
		             		can_resolve = convention.CanResolve(container_mock.Object, service);
		             	};

		It should_be_true = () => can_resolve.ShouldBeFalse();
	}
}