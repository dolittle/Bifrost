using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf
{
    [Subject(typeof(OrderedInstancesOf<>))]
    public sealed class when_there_are_no_instances : given.an_ordered_instances_of
    {
        Establish context = () => Register();

        Because of = () => result = ordered_instances_of.ToArray();

        It should_not_return_anything = () => result.ShouldBeEmpty();
    }
}