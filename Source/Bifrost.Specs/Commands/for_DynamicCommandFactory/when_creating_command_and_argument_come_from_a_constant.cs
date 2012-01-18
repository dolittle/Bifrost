using System;
using Bifrost.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_DynamicCommandFactory
{
    public class when_creating_command_and_argument_come_from_a_constant : given.a_dynamic_command_factory
    {
        const string expected = "Something";
        static dynamic command;

        Because of = () => command = (dynamic)factory.Create<StatelessAggregatedRootWithOneMethod>(Guid.NewGuid(), a => a.DoSomething(expected));

        It should_have_the_value_from_the_constant = () => ((string)command.Input).ShouldEqual(expected);
    }
}