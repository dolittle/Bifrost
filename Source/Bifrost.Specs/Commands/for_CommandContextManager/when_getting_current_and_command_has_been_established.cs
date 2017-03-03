using System;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
    [Subject(Subjects.getting_context)]
    public class when_getting_current_and_command_has_not_been_established : given.a_command_context_manager
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => Manager.GetCurrent());

        It should_throw_invalid_operation_exception = () => exception.ShouldBeOfExactType(typeof(InvalidOperationException));
    }
}