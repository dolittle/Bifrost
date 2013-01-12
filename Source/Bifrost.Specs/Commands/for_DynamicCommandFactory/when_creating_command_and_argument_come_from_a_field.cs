using System;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_DynamicCommandFactory
{
    public class when_creating_command_and_argument_come_from_a_field : given.a_dynamic_command_factory
    {
        static string expected = "Something";
        static dynamic command;

        Because of = () => command = (dynamic)factory.Create<StatelessAggregatedRootWithOneMethod>(Guid.NewGuid(), a => a.DoSomething(expected));

        It should_have_the_value_from_the_field = () => ((string)command.Input).ShouldEqual(expected);
    }
}