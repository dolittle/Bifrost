using Bifrost.Commands;
using Machine.Specifications;
using System;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_null_command_result : given.a_command_statistics
    {
        static CommandResult commandResult = null;
        static Exception thrown_exception;

        Because of = () => thrown_exception = Catch.Exception(() => command_statistics.Record(commandResult));

        It should_throw_null_argument_exception = () => thrown_exception.ShouldBeOfType<ArgumentNullException>();
    }
}
