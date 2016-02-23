using System;
using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf
{
    [Subject(typeof(OrderedInstancesOf<>))]
    public sealed class when_instances_have_circular_dependencies : given.an_ordered_instances_of
    {
        static Exception exception;

        [After(typeof(Dummy2))]
        class Dummy1 : IDummy { }

        [After(typeof(Dummy1))]
        class Dummy2 : IDummy { }

        static readonly IDummy dummy1 = new Dummy1();
        static readonly IDummy dummy2 = new Dummy2();

        Establish context = () => Register(dummy1, dummy2);

        Because of = () => exception = Catch.Only<CyclicDependencyException>(() => ordered_instances_of.ToArray());

        It should_throw_exception = () => exception.Message.ShouldContain("circular");

        It should_not_have_created_the_instances = () =>
            container_mock.Verify(m => m.Get(Moq.It.IsAny<Type>()), Times.Never);
    }
}