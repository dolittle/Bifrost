using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ContractToImplementorsMap
{
    public class when_feeding_two_times : given.an_empty_map
    {
        Establish context = () => map.Feed(new[] { typeof(ImplementationOfInterface)  });

        Because of = () => map.Feed(new[] { typeof(SecondImplementationOfInterface) });

        It should_have_both_the_implementations_only = () => map.GetImplementorsFor(typeof(IInterface)).ShouldContainOnly(typeof(ImplementationOfInterface), typeof(SecondImplementationOfInterface));
    }
}
