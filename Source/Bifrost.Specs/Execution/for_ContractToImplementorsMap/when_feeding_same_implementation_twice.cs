using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ContractToImplementorsMap
{
    public class when_feeding_same_implementation_twice : given.an_empty_map
    {
        Establish context = () => map.Feed(new[] { typeof(ImplementationOfInterface)  });

        Because of = () => map.Feed(new[] { typeof(ImplementationOfInterface) });

        It should_only_return_one_implementor = () => map.GetImplementorsFor(typeof(IInterface)).Count().ShouldEqual(1);
    }
}
