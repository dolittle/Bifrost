using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
	[Subject(typeof(TypeDiscoverer))]
	public class when_finding_types_with_multiple_implementations : given.a_type_discoverer
	{
		static Type[] typesFound;
		Because we_find_multiple = () => typesFound = TypeDiscoverer.FindMultiple<IMultiple>();

		It should_not_return_null = () => typesFound.ShouldNotBeNull();
		It should_return_2_types = () => typesFound.Length.ShouldEqual(2);
		It should_contain_the_expected_types = () => typesFound.ShouldContain(typeof (FirstMultiple), typeof (SecondMultiple));
	}
}