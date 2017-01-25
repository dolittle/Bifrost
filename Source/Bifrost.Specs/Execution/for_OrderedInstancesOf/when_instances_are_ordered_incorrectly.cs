using System;
using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf
{
    [Subject(typeof(OrderedInstancesOf<>))]
    public sealed class when_instances_are_ordered_incorrectly : given.an_ordered_instances_of
    {
        static Exception exception;

        [Order(1000)]
        class Dummy1 : IDummy { }

        [Order(3)]
        [After(typeof(Dummy1))]
        class Dummy2 : IDummy { }

        static readonly IDummy dummy1 = new Dummy1();
        static readonly IDummy dummy2 = new Dummy2();

        Establish context = () => Register(dummy2, dummy1);

        Because of = () => result = ordered_instances_of.ToArray();

        It should_return_correct_order = () => result.SequenceEqual(new[] { dummy1, dummy2 }).ShouldBeTrue();
    }
}