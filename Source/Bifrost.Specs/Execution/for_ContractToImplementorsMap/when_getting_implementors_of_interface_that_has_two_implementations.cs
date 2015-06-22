using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ContractToImplementorsMap
{
    public class when_getting_implementors_of_interface_that_has_two_implementations : given.an_empty_map
    {
        static IEnumerable<Type> result;

        Establish context = () => map.Feed(new[] { typeof(ImplementationOfInterface), typeof(SecondImplementationOfInterface) });

        Because of = () => result = map.GetImplementorsFor(typeof(IInterface));

        It should_have_both_the_implementations_only = () => result.ShouldContainOnly(typeof(ImplementationOfInterface), typeof(SecondImplementationOfInterface));
    }
}
