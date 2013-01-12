using System;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_DynamicCommandFactory
{
    public class when_creating_command_and_argument_come_from_another_method : given.a_dynamic_command_factory
    {
        static bool GetInputCalled = false;
        static string GetInput(string input)
        {
            GetInputCalled = true;
            return input;
        }

        const string expected = "Something";
        static dynamic command;

        Because of = () => command = (dynamic)factory.Create<StatelessAggregatedRootWithOneMethod>(Guid.NewGuid(), a => a.DoSomething(GetInput(expected)));

        It should_call_the_method = () => GetInputCalled.ShouldBeTrue();
        It should_have_the_value_from_the_method = () => ((string)command.Input).ShouldEqual(expected);
    }
}
