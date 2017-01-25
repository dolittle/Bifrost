using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf
{
    [Subject(typeof(OrderedInstancesOf<>))]
    public sealed class when_instances_are_ordered : given.an_ordered_instances_of
    {
        [Order(1)]
        class Dummy1 : IDummy { }

        [Order(2)]
        class Dummy2 : IDummy { }

        [Order(3)]
        class Dummy3 : IDummy { }

        static readonly IDummy dummy1 = new Dummy1();
        static readonly IDummy dummy2 = new Dummy2();
        static readonly IDummy dummy3 = new Dummy3();

        Establish context = () => Register(dummy3, dummy2, dummy1);

        Because of = () => result = ordered_instances_of.ToArray();

        It should_return_correct_order = () => result.SequenceEqual(new[] {dummy1, dummy2, dummy3 }).ShouldBeTrue();
    }
}