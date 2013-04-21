using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
	[Subject(typeof(TypeDiscoverer))]
	public class when_finding_types_with_multiple_implementations_but_asking_for_a_single : given.a_type_discoverer
	{
		static Exception Exception;
		Because we_ask_for_a_single = () => Exception = Catch.Exception(() => TypeDiscoverer.FindSingle<IMultiple>());

		It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
	}
}