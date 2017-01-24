using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf
{
    [Subject(typeof(OrderedInstancesOf<>))]
    public sealed class when_instances_have_dependencies : given.an_ordered_instances_of
    {
        class Dummy1 : IDummy { }

        [After(typeof(Dummy1))]
        class Dummy2 : IDummy { }

        [After(typeof(Dummy2))]
        class Dummy3 : IDummy { }

        static readonly IDummy dummy1 = new Dummy1();
        static readonly IDummy dummy2 = new Dummy2();
        static readonly IDummy dummy3 = new Dummy3();

        Establish context = () => Register(dummy3, dummy2, dummy1);

        Because of = () => result = ordered_instances_of.ToArray();

        It should_return_correct_order = () => result.SequenceEqual(new[] {dummy1, dummy2, dummy3 }).ShouldBeTrue();
    }
}