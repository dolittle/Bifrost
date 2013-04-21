using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
	[Subject(typeof(TypeDiscoverer))]
	public class when_finding_types_with_only_one_implementation : given.a_type_discoverer
	{
		static Type typeFound;
		Because we_find_single = () => typeFound = TypeDiscoverer.FindSingle<ISingle>();

		It should_not_return_null = () => typeFound.ShouldNotBeNull();
		It should_return_correct_implementation_when = () => typeFound.ShouldEqual(typeof (Single));
	}
}