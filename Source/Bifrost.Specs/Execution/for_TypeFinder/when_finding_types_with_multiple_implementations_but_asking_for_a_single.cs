using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_finding_types_with_multiple_implementations_but_asking_for_a_single : given.a_type_finder
	{
		static Exception exception;

        Because of = () => exception = Catch.Exception(() => type_finder.FindSingle<IMultiple>(types));

        It should_throw_an_ArgumentException = () => exception.ShouldBeOfExactType<MultipleTypesFoundException>();
	}
}