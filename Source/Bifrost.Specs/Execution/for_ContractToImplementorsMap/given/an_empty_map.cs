using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ContractToImplementorsMap.given
{
    public class an_empty_map
    {
        protected static ContractToImplementorsMap map;

        Establish context = () => map = new ContractToImplementorsMap();
    }
}
