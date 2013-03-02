using Bifrost.Commands;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_adding_a_null_had_validation_error_command : given.a_command_statistics
    {
        static Command command = null;
        static Exception thrown_exception; 

        Because of = () => thrown_exception = Catch.Exception(() => command_statistics.HadValidationError(command));

        It should_throw_null_argument_exception = () => thrown_exception.ShouldBeOfType<ArgumentNullException>();
    }
}
